using System.Threading;
using System.Threading.Tasks;
using VoicevoxClientSharp.Models;

namespace VoicevoxClientSharp.ApiClient
{
    public interface ISynthesisClient
    {
        /// <summary>
        /// 音声合成する
        /// </summary>
        /// <param name="speakerId"></param>
        /// <param name="audioQuery"></param>
        /// <param name="enableInterrogativeUpspeak">疑問系のテキストが与えられたら語尾を自動調整する</param>
        /// <param name="coreVersion"></param>
        /// <param name="ct"></param>
        /// <returns>wav</returns>
        ValueTask<byte[]> PostSynthesisAsync(int speakerId,
            AudioQuery audioQuery,
            bool? enableInterrogativeUpspeak = true,
            string? coreVersion = "",
            CancellationToken ct = default);

        /// <summary>
        /// 音声合成する（キャンセル可能）
        /// この API は実験的機能であり、エンジン起動時に引数で--enable_cancellable_synthesisを指定しないと有効化されません。
        /// </summary>
        /// <param name="speakerId"></param>
        /// <param name="audioQuery"></param>
        /// <param name="enableInterrogativeUpspeak">疑問系のテキストが与えられたら語尾を自動調整する</param>
        /// <param name="coreVersion"></param>
        /// <param name="ct"></param>
        /// <returns>wav</returns>
        ValueTask<byte[]> PostCancellableSynthesisAsync(int speakerId,
            AudioQuery audioQuery,
            bool? enableInterrogativeUpspeak = true,
            string? coreVersion = "",
            CancellationToken ct = default);

        /// <summary>
        /// 複数まとめて音声合成する
        /// </summary>
        /// <param name="speakerId"></param>
        /// <param name="audioQueries"></param>
        /// <param name="coreVersion"></param>
        /// <param name="ct"></param>
        /// <returns>zip圧縮されたwavファイル群</returns>
        ValueTask<byte[]> PostMultiSpeakerSynthesisAsync(int speakerId,
            AudioQuery[] audioQueries,
            string? coreVersion = "",
            CancellationToken ct = default);

        ValueTask PostFrameSynthesisAsync();
        ValueTask PostMorphableTargetsAsync();
        ValueTask PostSynthesisMorphingAsync();
    }

    public partial class RawApiClient
    {
        public ValueTask<byte[]> PostSynthesisAsync(int speakerId,
            AudioQuery audioQuery,
            bool? enableInterrogativeUpspeak,
            string? coreVersion = "",
            CancellationToken ct = default)
        {
            var queryString = QueryString(
                ("speaker", speakerId.ToString()),
                ("core_version", coreVersion),
                ("enable_interrogative_upspeak", enableInterrogativeUpspeak?.ToString())
            );
            var url = $"{_baseUrl}/synthesis?{queryString}";
            return PostAndByteResponseAsync(url, audioQuery, ct);
        }

        public ValueTask<byte[]> PostCancellableSynthesisAsync(int speakerId,
            AudioQuery audioQuery,
            bool? enableInterrogativeUpspeak,
            string? coreVersion = "",
            CancellationToken ct = default)
        {
            var queryString = QueryString(
                ("speaker", speakerId.ToString()),
                ("core_version", coreVersion),
                ("enable_interrogative_upspeak", enableInterrogativeUpspeak?.ToString())
            );
            var url = $"{_baseUrl}/cancellable_synthesis?{queryString}";
            return PostAndByteResponseAsync(url, audioQuery, ct);
        }

        public ValueTask<byte[]> PostMultiSpeakerSynthesisAsync(int speakerId,
            AudioQuery[] audioQueries,
            string? coreVersion = "",
            CancellationToken ct = default)
        {
            var queryString = QueryString(
                ("speaker", speakerId.ToString()),
                ("core_version", coreVersion)
            );
            var url = $"{_baseUrl}/multi_synthesis?{queryString}";
            return PostAndByteResponseAsync(url, audioQueries, ct);
        }


        public ValueTask PostFrameSynthesisAsync()
        {
            throw new System.NotImplementedException();
        }

        public ValueTask PostMorphableTargetsAsync()
        {
            throw new System.NotImplementedException();
        }

        public ValueTask PostSynthesisMorphingAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}