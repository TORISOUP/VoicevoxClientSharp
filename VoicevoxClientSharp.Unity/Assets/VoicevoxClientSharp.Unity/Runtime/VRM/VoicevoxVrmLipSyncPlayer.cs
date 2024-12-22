#if USE_VRM

using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UniVRM10;
using VoicevoxClientSharp.ApiClient.Models;

namespace VoicevoxClientSharp.Unity
{
    public sealed class VoicevoxVrmLipSyncPlayer : OptionalVoicevoxPlayer
    {
        /// <summary>
        /// VRMを指定する
        /// 再生中にVrm10Instanceを差し替えた場合の挙動は保障できない
        /// </summary>
        public Vrm10Instance VrmInstance;

        /// <summary>
        /// 口の空き具合のスケール
        /// </summary>
        public float MouthOpenScale = 1.0f;

        private readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);

        /// <summary>
        /// 再生開始からの本来の合計時間
        /// </summary>
        private decimal _expectedTotalTime = 0M;

        /// <summary>
        /// 再生開始からのUnity上での合計時間
        /// </summary>
        private decimal _accurateTotalTime = 0M;

        private readonly Dictionary<ExpressionKey, float> _expressionWeights = new Dictionary<ExpressionKey, float>();

        /// <summary>
        /// 現在のテキスト
        /// </summary>
        public string CurrentText { get; private set; } = "";

        /// <summary>
        /// 現在のモーラ
        /// </summary>
        public string CurrentMora { get; private set; } = "";

        private void Initialize()
        {
            if (VrmInstance == null)
            {
                VrmInstance = GetComponent<Vrm10Instance>();
            }
        }

        public override async UniTask PlayAsync(SynthesisResult synthesisResult, CancellationToken cancellationToken)
        {
            Initialize();

            // 外から渡されたCancellationTokenと、VoicevoxVrmLipSyncPlayerと、Vrm10InstanceのCancellationTokenを合成
            using var lcts = CancellationTokenSource.CreateLinkedTokenSource(
                cancellationToken,
                this.GetCancellationTokenOnDestroy(),
                VrmInstance.GetCancellationTokenOnDestroy());

            try
            {
                await _semaphoreSlim.WaitAsync(lcts.Token);
                IsPlaying = true;
                CurrentText = synthesisResult.Text;
                await LipSyncAsync(synthesisResult, VrmInstance.Runtime.Expression, lcts.Token);
            }
            finally
            {
                CurrentText = "";
                CurrentMora = "";
                IsPlaying = false;
                try
                {
                    _semaphoreSlim.Release();
                }
                catch
                {
                    // ignored
                }
            }
        }

        private async UniTask LipSyncAsync(
            SynthesisResult result,
            Vrm10RuntimeExpression expression,
            CancellationToken ct)
        {
            _accurateTotalTime = 0M;
            _expectedTotalTime = 0M;
            _expressionWeights[ExpressionKey.Aa] = 0.0f;
            _expressionWeights[ExpressionKey.Ih] = 0.0f;
            _expressionWeights[ExpressionKey.Ou] = 0.0f;
            _expressionWeights[ExpressionKey.Ee] = 0.0f;
            _expressionWeights[ExpressionKey.Oh] = 0.0f;

            expression.SetWeightsNonAlloc(_expressionWeights);

            var audioQuery = result.AudioQuery;

            // prePhonemeLength分だけ待機
            var waitStartTime = Time.time;
            await UniTask.Delay(TimeSpan.FromSeconds((double)(audioQuery.PrePhonemeLength / audioQuery.SpeedScale)),
                DelayType.Realtime,
                cancellationToken: ct);
            _expectedTotalTime += audioQuery.PrePhonemeLength / audioQuery.SpeedScale;
            _accurateTotalTime += (decimal)(Time.time - waitStartTime);

            try
            {
                foreach (var accentPhrase in audioQuery.AccentPhrases)
                {
                    foreach (var mora in accentPhrase.Moras)
                    {
                        // モーラを再生
                        CurrentMora = mora.Text;
                        await PlayMoraAsync(mora, audioQuery.SpeedScale, expression, ct);
                    }

                    if (accentPhrase.PauseMora != null)
                    {
                        CurrentMora = "";
                        // PauseMoraを再生
                        await PlayPauseMoraAsync(
                            accentPhrase.PauseMora,
                            audioQuery.SpeedScale,
                            audioQuery.PauseLength,
                            audioQuery.PauseLengthScale,
                            expression, ct);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // 途中キャンセルされたら口を閉じる
                SetFaceToNeutral();
                expression.SetWeightsNonAlloc(_expressionWeights);
                CurrentText = "";

                throw;
            }

            // 最後までいったら閉じる
            SetFaceToNeutral();
            expression.SetWeightsNonAlloc(_expressionWeights);
            CurrentText = "";

            // postPhonemeLength分だけ待機
            waitStartTime = Time.time;
            await UniTask.Delay(TimeSpan.FromSeconds((double)(audioQuery.PostPhonemeLength / audioQuery.SpeedScale)),
                cancellationToken: ct);
            _expectedTotalTime += audioQuery.PostPhonemeLength / audioQuery.SpeedScale;
            _accurateTotalTime += (decimal)(Time.time - waitStartTime);
        }


        private async UniTask PlayMoraAsync(
            Mora mora,
            decimal speedScale,
            Vrm10RuntimeExpression expression,
            CancellationToken ct)
        {
            // 子音が存在する場合はそれを再生反映する
            if (mora.Consonant != null)
            {
                await PlayConsonantAsync(mora.Consonant, mora.ConsonantLength!.Value, speedScale, expression, ct);
            }

            // 母音を再生反映する
            await PlayVowelAsync(mora.Vowel, mora.VowelLength, speedScale, expression, ct);
        }

        /// <summary>
        /// 子音を再生する
        /// </summary>
        private async UniTask PlayConsonantAsync(
            string consonant,
            decimal consonantLength,
            decimal speedScale,
            Vrm10RuntimeExpression expression,
            CancellationToken ct)
        {
            // 破裂音系か？
            var isNeutral = consonant.ToLower() is "m" or "b" or "p" or "f" or "v" or "w";

            var elapsedTime = 0M;

            // 今の数値
            var aa = _expressionWeights[ExpressionKey.Aa];
            var ih = _expressionWeights[ExpressionKey.Ih];
            var ou = _expressionWeights[ExpressionKey.Ou];
            var ee = _expressionWeights[ExpressionKey.Ee];
            var oh = _expressionWeights[ExpressionKey.Oh];

            var length = consonantLength / speedScale;
            var targetElapsedTime = _expectedTotalTime + length;
            var waitingTime = (decimal)Mathf.Max((float)(targetElapsedTime - _accurateTotalTime), 0);
            var startTime = Time.time;

            while ((Time.time - startTime) < (float)waitingTime)
            {
                // 破裂音系の場合のみ動かす
                if (isNeutral)
                {
                    // 指定フレームを消費して口を閉じる
                    _expressionWeights[ExpressionKey.Aa] = Mathf.Lerp(aa, 0.0f, (float)(elapsedTime / waitingTime));
                    _expressionWeights[ExpressionKey.Ih] = Mathf.Lerp(ih, 0.0f, (float)(elapsedTime / waitingTime));
                    _expressionWeights[ExpressionKey.Ou] = Mathf.Lerp(ou, 0.0f, (float)(elapsedTime / waitingTime));
                    _expressionWeights[ExpressionKey.Ee] = Mathf.Lerp(ee, 0.0f, (float)(elapsedTime / waitingTime));
                    _expressionWeights[ExpressionKey.Oh] = Mathf.Lerp(oh, 0.0f, (float)(elapsedTime / waitingTime));
                    expression.SetWeightsNonAlloc(_expressionWeights);
                }

                await UniTask.Yield(PlayerLoopTiming.Update, ct);
                elapsedTime += (decimal)Time.deltaTime;
            }

            if (isNeutral)
            {
                // 最後までいったら閉じる
                SetFaceToNeutral();
                expression.SetWeightsNonAlloc(_expressionWeights);
            }

            // 時間を加算
            _expectedTotalTime += length;
            _accurateTotalTime += elapsedTime;
        }


        /// <summary>
        /// 母音を再生する
        /// </summary>
        private async UniTask PlayVowelAsync(
            string vowel,
            decimal vowelLength,
            decimal speedScale,
            Vrm10RuntimeExpression expression,
            CancellationToken ct)
        {
            var elapsedTime = 0M;

            // 今の数値をちょっと小さくする
            var caa = _expressionWeights[ExpressionKey.Aa] * 0.8f;
            var cih = _expressionWeights[ExpressionKey.Ih] * 0.8f;
            var cou = _expressionWeights[ExpressionKey.Ou] * 0.8f;
            var cee = _expressionWeights[ExpressionKey.Ee] * 0.8f;
            var coh = _expressionWeights[ExpressionKey.Oh] * 0.8f;

            // 目標値
            var taa = 0f;
            var tih = 0f;
            var tou = 0f;
            var tee = 0f;
            var toh = 0f;

            switch (vowel.ToLower())
            {
                case "a":
                    taa = MouthOpenScale;
                    break;
                case "i":
                    tih = MouthOpenScale;
                    break;
                case "u":
                    tou = MouthOpenScale;
                    break;
                case "e":
                    tee = MouthOpenScale;
                    break;
                case "o":
                    toh = MouthOpenScale;
                    break;
                case "cl":
                    // 「っ」のときはちょっと閉じる
                    taa = caa * 0.8f;
                    tih = cih * 0.8f;
                    tou = cou * 0.8f;
                    tee = cee * 0.8f;
                    toh = coh * 0.8f;
                    break;
                default:
                    taa = 0;
                    tih = 0;
                    tou = 0;
                    tee = 0;
                    toh = 0;
                    break;
            }

            var length = vowelLength / speedScale;
            var targetElapsedTime = _expectedTotalTime + length;
            var waitingTime = (decimal)Mathf.Max((float)(targetElapsedTime - _accurateTotalTime), 0);
            var startTime = Time.time;

            while ((Time.time - startTime) < (float)waitingTime)
            {
                _expressionWeights[ExpressionKey.Aa] =
                    Mathf.Lerp(caa, taa, (float)(elapsedTime / waitingTime));
                _expressionWeights[ExpressionKey.Ih] =
                    Mathf.Lerp(cih, tih, (float)(elapsedTime / waitingTime));
                _expressionWeights[ExpressionKey.Ou] =
                    Mathf.Lerp(cou, tou, (float)(elapsedTime / waitingTime));
                _expressionWeights[ExpressionKey.Ee] =
                    Mathf.Lerp(cee, tee, (float)(elapsedTime / waitingTime));
                _expressionWeights[ExpressionKey.Oh] =
                    Mathf.Lerp(coh, toh, (float)(elapsedTime / waitingTime));
                expression.SetWeightsNonAlloc(_expressionWeights);

                await UniTask.Yield(PlayerLoopTiming.Update, ct);
                elapsedTime += (decimal)Time.deltaTime;
            }

            _expressionWeights[ExpressionKey.Aa] = taa;
            _expressionWeights[ExpressionKey.Ih] = tih;
            _expressionWeights[ExpressionKey.Ou] = tou;
            _expressionWeights[ExpressionKey.Ee] = tee;
            _expressionWeights[ExpressionKey.Oh] = toh;
            expression.SetWeightsNonAlloc(_expressionWeights);

            // 時間を加算
            _expectedTotalTime += length;
            _accurateTotalTime += elapsedTime;
        }

        /// <summary>
        /// PauseMoraとして再生する
        /// </summary>
        private async UniTask PlayPauseMoraAsync(
            Mora mora,
            decimal speedScale,
            decimal? pauseLength,
            decimal? pauseLengthScale,
            Vrm10RuntimeExpression expression,
            CancellationToken ct)
        {
            // PauseMoraが存在しない場合は何もしない
            if (mora == null) return;

            var elapsedTime = 0M;

            // 今の数値
            var aa = _expressionWeights[ExpressionKey.Aa];
            var ih = _expressionWeights[ExpressionKey.Ih];
            var ou = _expressionWeights[ExpressionKey.Ou];
            var ee = _expressionWeights[ExpressionKey.Ee];
            var oh = _expressionWeights[ExpressionKey.Oh];

            // 待機時間より短く口を閉じる
            var rate = 2.0f;

            var lenght = (pauseLength ?? mora.VowelLength) * (pauseLengthScale ?? 1M) / speedScale;

            // 予想終了時間
            var expectedEndTime = _expectedTotalTime + lenght;

            // 予想終了時間より、実際の待機時間を計算する
            var waitTime =　(decimal)Mathf.Max((float)(expectedEndTime - _accurateTotalTime), 0);

            var startTime = Time.time;

            while ((Time.time - startTime) < (float)waitTime)
            {
                _expressionWeights[ExpressionKey.Aa]
                    = Mathf.Lerp(aa, 0.0f, rate * (float)(elapsedTime / lenght));
                _expressionWeights[ExpressionKey.Ih]
                    = Mathf.Lerp(ih, 0.0f, rate * (float)(elapsedTime / lenght));
                _expressionWeights[ExpressionKey.Ou]
                    = Mathf.Lerp(ou, 0.0f, rate * (float)(elapsedTime / lenght));
                _expressionWeights[ExpressionKey.Ee]
                    = Mathf.Lerp(ee, 0.0f, rate * (float)(elapsedTime / lenght));
                _expressionWeights[ExpressionKey.Oh]
                    = Mathf.Lerp(oh, 0.0f, rate * (float)(elapsedTime / lenght));
                expression.SetWeightsNonAlloc(_expressionWeights);


                await UniTask.Yield(PlayerLoopTiming.Update, ct);
                elapsedTime += (decimal)Time.deltaTime;
            }

            // 最後までいったら閉じる
            SetFaceToNeutral();
            expression.SetWeightsNonAlloc(_expressionWeights);

            // 時間を加算
            _expectedTotalTime += lenght;
            _accurateTotalTime += elapsedTime;
        }

        private void SetFaceToNeutral()
        {
            _expressionWeights[ExpressionKey.Aa] = 0.0f;
            _expressionWeights[ExpressionKey.Ih] = 0.0f;
            _expressionWeights[ExpressionKey.Ou] = 0.0f;
            _expressionWeights[ExpressionKey.Ee] = 0.0f;
            _expressionWeights[ExpressionKey.Oh] = 0.0f;
        }


        private void OnDestroy()
        {
            _expressionWeights.Clear();
            try
            {
                _semaphoreSlim.Dispose();
            }
            catch
            {
                // ignored
            }
        }
    }
}
#endif