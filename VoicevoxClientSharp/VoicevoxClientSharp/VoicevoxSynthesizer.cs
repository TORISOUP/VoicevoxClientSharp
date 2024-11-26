using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VoicevoxClientSharp.ApiClient;
using VoicevoxClientSharp.ApiClient.Models;

namespace VoicevoxClientSharp
{
    /// <summary>
    /// VOICEVOXの音声合成を簡易に扱うためのラッパークラス
    /// </summary>
    public sealed class VoicevoxSynthesizer : IDisposable
    {
        private readonly object _gate = new object();
        private bool _isDisposed = false;
        private readonly IVoicevoxApiClient _apiClient;
        private readonly bool _handleRawApiClient = false;
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();

        /// <summary>
        /// VOICEVOXで発話音声の合成を制御するクラス
        /// </summary>
        public VoicevoxSynthesizer()
        {
            _apiClient = new VoicevoxApiClient();
            _handleRawApiClient = true;
        }

        /// <summary>
        /// VOICEVOXで発話音声の合成を制御するクラス
        /// </summary>
        /// <param name="apiClient">ここで指定したIVoicevoxApiClientのDispose()呼び出しはしません。手動で寿命管理してください。</param>
        public VoicevoxSynthesizer(IVoicevoxApiClient apiClient)
        {
            _apiClient = apiClient;
            _handleRawApiClient = false;
        }

        /// <summary>
        /// Speaker一覧を取得します。
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async ValueTask<Speaker[]> GetSpeakersAsync(CancellationToken ct = default)
        {
            ThrowIfDisposed();

            using var lcts = CancellationTokenSource.CreateLinkedTokenSource(_cts.Token, ct);
            return await _apiClient.GetSpeakersAsync(ct: lcts.Token);
        }

        /// <summary>
        /// スタイルを初期化します。
        /// 
        /// 初期化しなくても発話は可能ですが、初回実行時に時間がかかることがあります。
        /// </summary>
        /// <param name="styleId"></param>
        /// <param name="ct"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public async ValueTask InitializeStyleAsync(int styleId, CancellationToken ct = default)
        {
            ThrowIfDisposed();

            using var lcts = CancellationTokenSource.CreateLinkedTokenSource(_cts.Token, ct);
            var isInitialized = await _apiClient.IsInitializedSpeakerAsync(styleId, ct: lcts.Token);
            if (!isInitialized)
            {
                await _apiClient.InitializeSpeakerAsync(styleId, ct: lcts.Token);
            }
        }

        /// <summary>
        /// StyleIdをSpeakerNameとStyleNameから検索します。
        /// </summary>
        /// <param name="speakerName">スピーカー名 例:四国めたん</param>
        /// <param name="styleName">スタイル名 例:あまあま</param>
        /// <param name="ct"></param>
        /// <returns>見つけた場合はSpeakerId、見つからなければnull</returns>
        public async ValueTask<int?> FindStyleIdByNameAsync(
            string speakerName,
            string styleName,
            CancellationToken ct = default)
        {
            ThrowIfDisposed();

            var speakers = await GetSpeakersAsync(ct);
            var speaker = speakers.FirstOrDefault(s => s.Name == speakerName);
            var style = speaker?.Styles.FirstOrDefault(x => x.Name == styleName);
            return style?.Id;
        }

        /// <summary>
        /// 指定スタイルIdで指定テキストを発話し、その結果をwavとして返します。
        /// </summary>
        /// <param name="styleId">スタイルId</param>
        /// <param name="text">発話内容</param>
        /// <param name="speedScale">全体の話速、 推奨:0.5~2.0</param>
        /// <param name="pitchScale">全体の音高、 推奨:-0.15~0.15</param>
        /// <param name="intonationScale">全体の抑揚、推奨:0.0~2.0</param>
        /// <param name="volumeScale">全体の音量</param>
        /// <param name="prePhonemeLength">音声の前の無音時間</param>
        /// <param name="postPhonemeLength">音声の後の無音時間</param>
        /// <param name="pauseLength">句読点などの無音時間。nullのときは無視される。</param>
        /// <param name="pauseLengthScale">句読点などの無音時間（倍率）。</param>
        /// <param name="ct"></param>
        /// <returns>wavデータ</returns>
        public async ValueTask<SynthesisResult> SynthesizeSpeechAsync(
            int styleId,
            string text,
            decimal speedScale = 1M,
            decimal pitchScale = 0M,
            decimal intonationScale = 1M,
            decimal volumeScale = 1M,
            decimal prePhonemeLength = 0.1M,
            decimal postPhonemeLength = 0.1M,
            decimal? pauseLength = null,
            decimal? pauseLengthScale = 1M,
            CancellationToken ct = default)
        {
            ThrowIfDisposed();

            using var lcts = CancellationTokenSource.CreateLinkedTokenSource(_cts.Token, ct);

            // 音声合成クエリを作成
            var audioQuery = await _apiClient.CreateAudioQueryAsync(text, styleId, ct: lcts.Token);

            // 音声クエリを指定パラメータで上書き
            audioQuery.SpeedScale = speedScale;
            audioQuery.PitchScale = pitchScale;
            audioQuery.IntonationScale = intonationScale;
            audioQuery.VolumeScale = volumeScale;
            audioQuery.PrePhonemeLength = prePhonemeLength;
            audioQuery.PostPhonemeLength = postPhonemeLength;
            audioQuery.PauseLength = pauseLength;
            audioQuery.PauseLengthScale = pauseLengthScale;

            // wavの作成
            var wav = await _apiClient.SynthesisAsync(styleId, audioQuery, ct: lcts.Token);
            return new SynthesisResult(wav, audioQuery);
        }

        /// <summary>
        /// VOICEVOXに登録されたプリセットを使って発話し、その結果をwavとして返します。
        /// プリセットが存在しない場合は
        /// 
        /// プリセット情報の取得・更新・削除はIPresetClientを使って行ってください。
        /// </summary>
        /// <param name="presetId">プリセットId</param>
        /// <param name="text">発話内容</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async ValueTask<SynthesisResult> SynthesizeSpeechWithPresetAsync(
            int presetId,
            string text,
            CancellationToken ct = default)
        {
            ThrowIfDisposed();
            using var lcts = CancellationTokenSource.CreateLinkedTokenSource(_cts.Token, ct);
            var audioQuery = await _apiClient.CreateAudioQueryFromPresetAsync(text, presetId, ct: lcts.Token);
            var wav = await _apiClient.SynthesisAsync(presetId, audioQuery, ct: lcts.Token);
            return new SynthesisResult(wav, audioQuery);
        }


        /// <summary>
        /// 2つのスタイルを合成して発話し、その結果をwavとして返します。
        /// スタイルが合成可能であるかはCanMorphAsyncで確認してください。
        /// </summary>
        /// <param name="baseStyleId">スタイルId</param>
        /// <param name="targetStyleId">合成するスタイルId</param>
        /// <param name="rate">合成の割合。0.0でベース、1.0でターゲットに近づく。</param>
        /// <param name="text">発話内容</param>
        /// <param name="speedScale">全体の話速、 推奨:0.5~2.0</param>
        /// <param name="pitchScale">全体の音高、 推奨:-0.15~0.15</param>
        /// <param name="intonationScale">全体の抑揚、推奨:0.0~2.0</param>
        /// <param name="volumeScale">全体の音量</param>
        /// <param name="prePhonemeLength">音声の前の無音時間</param>
        /// <param name="postPhonemeLength">音声の後の無音時間</param>
        /// <param name="pauseLength">句読点などの無音時間。nullのときは無視される。</param>
        /// <param name="pauseLengthScale">句読点などの無音時間（倍率）。</param>
        /// <param name="ct"></param>
        /// <returns>wavデータ</returns>
        public async ValueTask<SynthesisResult> SpeakMorphingAsync(
            int baseStyleId,
            int targetStyleId,
            decimal rate,
            string text,
            decimal speedScale = 1M,
            decimal pitchScale = 0M,
            decimal intonationScale = 1M,
            decimal volumeScale = 1M,
            decimal prePhonemeLength = 0.1M,
            decimal postPhonemeLength = 0.1M,
            decimal? pauseLength = null,
            decimal? pauseLengthScale = 1M,
            CancellationToken ct = default)
        {
            ThrowIfDisposed();

            using var lcts = CancellationTokenSource.CreateLinkedTokenSource(_cts.Token, ct);

            // 音声合成クエリを作成
            var audioQuery = await _apiClient.CreateAudioQueryAsync(text, baseStyleId, ct: lcts.Token);

            // 音声クエリを指定パラメータで上書き
            audioQuery.SpeedScale = speedScale;
            audioQuery.PitchScale = pitchScale;
            audioQuery.IntonationScale = intonationScale;
            audioQuery.VolumeScale = volumeScale;
            audioQuery.PrePhonemeLength = prePhonemeLength;
            audioQuery.PostPhonemeLength = postPhonemeLength;
            audioQuery.PauseLength = pauseLength;
            audioQuery.PauseLengthScale = pauseLengthScale;

            // wavの作成
            var wav = await _apiClient.SynthesisMorphingAsync(baseStyleId, targetStyleId, rate, audioQuery,
                ct: lcts.Token);
            return new SynthesisResult(wav, audioQuery);
        }

        /// <summary>
        /// 2つのスタイルが合成可能であるかを返します。
        /// </summary>
        /// <param name="baseStyleId">ベース</param>
        /// <param name="targetStyleId">ターゲット</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async ValueTask<bool> CanMorphAsync(int baseStyleId, int targetStyleId, CancellationToken ct = default)
        {
            ThrowIfDisposed();
            using var lcts = CancellationTokenSource.CreateLinkedTokenSource(_cts.Token, ct);
            var result = await _apiClient.IsMorphableTargetsAsync(new[] { baseStyleId }, ct: lcts.Token);

            if (result.Length == 0) return false;

            var dict = result[0];
            if (dict.TryGetValue(targetStyleId.ToString(), out var morphableTargetInfo))
            {
                return morphableTargetInfo.IsMorphable;
            }

            return false;
        }


        private void ThrowIfDisposed()
        {
            lock (_gate)
            {
                if (_isDisposed)
                {
                    throw new ObjectDisposedException(nameof(VoicevoxSynthesizer));
                }
            }
        }

        public void Dispose()
        {
            lock (_gate)
            {
                if (_isDisposed)
                {
                    return;
                }

                _isDisposed = true;

                _cts.Cancel();
                _cts.Dispose();
                if (_handleRawApiClient)
                {
                    _apiClient.Dispose();
                }
            }
        }
    }

    /// <summary>
    /// VOICEVOXの発話音声の合成結果
    /// </summary>
    public readonly struct SynthesisResult : IEquatable<SynthesisResult>
    {
        /// <summary>
        /// 合成した音声データ
        /// </summary>
        public byte[] Wav { get; }

        /// <summary>
        /// 音声合成に使用したクエリ
        /// </summary>
        public AudioQuery AudioQuery { get; }

        public SynthesisResult(byte[] wav, AudioQuery audioQuery)
        {
            Wav = wav;
            AudioQuery = audioQuery;
        }

        public bool Equals(SynthesisResult other)
        {
            return Wav.Equals(other.Wav) && AudioQuery.Equals(other.AudioQuery);
        }

        public override bool Equals(object? obj)
        {
            return obj is SynthesisResult other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Wav.GetHashCode() * 397) ^ AudioQuery.GetHashCode();
            }
        }
        
        public void Deconstruct(out byte[] wav, out AudioQuery audioQuery)
        {
            wav = Wav;
            audioQuery = AudioQuery;
        }
    }
}