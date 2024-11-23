using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VoicevoxClientSharp.Unity.Utilities;

namespace VoicevoxClientSharp.Unity
{
    public class VoicevoxSpeakPlayer : MonoBehaviour
    {
        /// <summary>
        /// 再生に用いるAudioSource
        /// </summary>
        public AudioSource AudioSource;

        /// <summary>
        /// 再生時に同時に使用するオプション
        /// </summary>
        public List<OptionalVoicevoxPlayer> OptionalVoicevoxPlayers;

        private readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);
        private bool _isDestroyed;
        public bool IsPlaying { get; private set; }

        /// <summary>
        /// SynthesisResultで音声の再生を行う
        /// またOptionalVoicevoxPlayerを指定している場合はそれら全てを同時に実行し完了まで待機する
        /// </summary>
        public async UniTask PlayAsync(SynthesisResult result, CancellationToken ct)
        {
            using var linkedCts =
                CancellationTokenSource.CreateLinkedTokenSource(ct, this.GetCancellationTokenOnDestroy());
            var ct2 = linkedCts.Token;

            // 1つのVoicevoxSpeakPlayerで同時に1つの音声しか再生できないようにする
            await _semaphoreSlim.WaitAsync(ct2);

            // WavデータをAudioClipに変換
            var audioClip = AudioUtility.CreateAudioClipFromWav(result.Wav);

            try
            {
                IsPlaying = true;

                // 再生
                AudioSource.clip = audioClip;
                AudioSource.Play();

                // 再生完了まで待機
                if (OptionalVoicevoxPlayers == null || OptionalVoicevoxPlayers.Count == 0)
                {
                    await UniTask.WaitUntil(() => !AudioSource.isPlaying, cancellationToken: ct2);
                }
                else
                {
                    // オプションの再生を同時に実行
                    var audioTask = UniTask.WaitUntil(() => !AudioSource.isPlaying, cancellationToken: ct2);
                    var optionalTasks = OptionalVoicevoxPlayers.Select(player => player.PlayAsync(result, ct2))
                        .ToArray();
                    await UniTask.WhenAll(optionalTasks.Append(audioTask).ToArray());
                }
            }
            finally
            {
                if (audioClip != null) Destroy(audioClip);
                if (!_isDestroyed) _semaphoreSlim.Release(1);
                IsPlaying = false;
            }
        }


        private void OnDestroy()
        {
            try
            {
                _isDestroyed = true;
                _semaphoreSlim.Dispose();
            }
            catch
            {
                //ignore
            }
        }
    }
}