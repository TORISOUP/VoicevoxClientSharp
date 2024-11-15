using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VoicevoxClientSharp.ApiClient;
using VoicevoxClientSharp.Models;

namespace VoicevoxClientSharp
{
    /// <summary>
    /// VOICEVOXの発話制御を簡易に行うためのPlayer
    /// スレッドセーフではない
    /// </summary>
    public sealed class VoicevoxSpeakPlayer : IDisposable
    {
        private bool _isDisposed = false;
        private readonly IVoicevoxRawApiClient _rawApiClient;
        private readonly bool _handleRawApiClient = false;
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();

        /// <summary>
        /// 現在設定されているSpeakerId
        /// </summary>
        public int SpeakerId { get; private set; } = 0;

        /// <summary>
        /// VOICEVOXの制御を簡易に行うためのPlayer
        /// </summary>
        public VoicevoxSpeakPlayer()
        {
            _rawApiClient = new RawRawApiClient();
            _handleRawApiClient = true;
        }

        /// <summary>
        /// VOICEVOXの制御を簡易に行うためのPlayer
        /// </summary>
        /// <param name="rawApiClient">ここで指定したIVoicevoxRawApiClientのDispose()呼び出しはしません。手動で寿命管理してください。</param>
        public VoicevoxSpeakPlayer(IVoicevoxRawApiClient rawApiClient)
        {
            _rawApiClient = rawApiClient;
            _handleRawApiClient = false;
        }


        /// <summary>
        /// Speaker一覧を取得します。
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async ValueTask<Speaker[]> GetSpeakerAsync(CancellationToken ct = default)
        {
            using var lcts = CancellationTokenSource.CreateLinkedTokenSource(_cts.Token, ct);
            return await _rawApiClient.GetSpeakersAsync(ct: lcts.Token);
        }

        /// <summary>
        /// Speakerを設定します。
        /// </summary>
        /// <param name="speakerId"></param>
        /// <param name="ct"></param>
        public async ValueTask SetCurrentSpeakerAsync(int speakerId, CancellationToken ct = default)
        {
            using var lcts = CancellationTokenSource.CreateLinkedTokenSource(_cts.Token, ct);
            var isInitialized = await _rawApiClient.IsInitializedSpeakerAsync(speakerId, ct: lcts.Token);
            if (!isInitialized)
            {
                await _rawApiClient.InitializeSpeakerAsync(speakerId, ct: lcts.Token);
            }

            SpeakerId = speakerId;
        }

        /// <summary>
        /// Speakerを名前とスタイル名から検索します。
        /// </summary>
        /// <param name="speakerName">スピーカー名</param>
        /// <param name="styleName">スタイル名</param>
        /// <param name="ct"></param>
        /// <returns>見つけた場合はSpeakerId、見つからなければnull</returns>
        public async ValueTask<int?> FindSpeakerIdByNameAsync(
            string speakerName = "四国めたん",
            string styleName = "ノーマル",
            CancellationToken ct = default)
        {
            var speakers = await GetSpeakerAsync(ct);
            var speaker = speakers.FirstOrDefault(s => s.Name == speakerName);
            var style = speaker?.Styles.FirstOrDefault(x => x.Name == styleName);
            return style?.Id;
        }

        public void Dispose()
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
                _rawApiClient.Dispose();
            }
        }
    }
}