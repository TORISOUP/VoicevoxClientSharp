using System;
using System.Threading;
using System.Threading.Tasks;
using VoicevoxClientSharp.Models;

namespace VoicevoxClientSharp.ApiClient
{
    public interface IQueryClient : IDisposable
    {
        /// <summary>
        /// POST /audio_query
        /// 音声合成用のクエリを作成する
        /// </summary>
        ValueTask<AudioQuery> CreateAudioQueryAsync(string text,
            int speakerId,
            string? coreVersion = null,
            CancellationToken ct = default);

        /// <summary>
        /// POST /audio_query_from_preset
        /// 音声合成用のクエリをプリセットを用いて作成する
        /// </summary>
        ValueTask<AudioQuery> CreateAudioQueryFromPresetAsync(string text,
            int presetId,
            string? coreVersion = null,
            CancellationToken ct = default);

        /// <summary>
        /// POST /sing_frame_audio_query
        /// 歌唱音声合成用のクエリを作成する
        /// </summary>
        ValueTask<FrameAudioQuery> CreateSingFrameAudioQueryAsync(Score score,
            int speakerId,
            string? coreVersion = null,
            CancellationToken ct = default);

        /// <summary>
        /// POST /accent_phrases
        /// テキストからアクセント句を得る
        /// </summary>
        ValueTask<AccentPhrase[]> CreateAccentPhraseAsync(string text,
            int speakerId,
            bool? isKana = false,
            string? coreVersion = null,
            CancellationToken ct = default);

        /// <summary>
        /// POST /mora_data
        /// アクセント句から音高・音素長を得る
        /// </summary>
        ValueTask<AccentPhrase[]> FetchMoraDataAsync(
            int speakerId,
            AccentPhrase[] accentPhrases,
            string? coreVersion = null,
            CancellationToken ct = default);

        /// <summary>
        /// POST /mora_length
        /// アクセント句から音素長を得る
        /// </summary>
        ValueTask<AccentPhrase[]> FetchMoraLengthAsync(
            int speakerId,
            AccentPhrase[] accentPhrases,
            string? coreVersion = null,
            CancellationToken ct = default);

        /// <summary>
        /// POST /mora_pitch
        /// アクセント句から音高を得る
        /// </summary>
        ValueTask<AccentPhrase[]> FetchMoraPitchAsync(
            int speakerId,
            AccentPhrase[] accentPhrases,
            string? coreVersion = null,
            CancellationToken ct = default);

        /// <summary>
        /// POST /sing_frame_volume
        /// 楽譜・歌唱音声合成用のクエリからフレームごとの音量を得る
        /// </summary>
        ValueTask<decimal[]> FetchSingFrameVolumeAsync(
            int speakerId,
            Score score,
            FrameAudioQuery frameAudioQuery,
            string? coreVersion = null,
            CancellationToken ct = default);
    }

    public partial class RawRawApiClient
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public ValueTask<AudioQuery> CreateAudioQueryAsync(string text,
            int speakerId,
            string? coreVersion = null,
            CancellationToken ct = default)
        {
            var queryString = CreateQueryString(
                ("text", text),
                ("speaker", speakerId.ToString()),
                ("core_version", coreVersion)
            );
            var url = $"{_baseUrl}/audio_query?{queryString}";
            return PostAsync<AudioQuery>(url, ct);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public ValueTask<AudioQuery> CreateAudioQueryFromPresetAsync(string text,
            int presetId,
            string? coreVersion = null,
            CancellationToken ct = default)
        {
            var queryString = CreateQueryString(
                ("text", text),
                ("preset_id", presetId.ToString()),
                ("core_version", coreVersion)
            );
            var url =
                $"{_baseUrl}/audio_query_from_preset?{queryString}";
            return PostAsync<AudioQuery>(url, ct);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public ValueTask<FrameAudioQuery> CreateSingFrameAudioQueryAsync(Score score,
            int speakerId,
            string? coreVersion = null,
            CancellationToken ct = default)
        {
            var queryString = CreateQueryString(
                ("speaker", speakerId.ToString()),
                ("core_version", coreVersion)
            );

            var url = $"{_baseUrl}/sing_frame_audio_query?{queryString}";
            return PostAsync<Score, FrameAudioQuery>(url, score, ct);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public ValueTask<AccentPhrase[]> CreateAccentPhraseAsync(string text,
            int speakerId,
            bool? isKana,
            string? coreVersion = null,
            CancellationToken ct = default)
        {
            var queryString = CreateQueryString(
                ("text", text),
                ("speaker", speakerId.ToString()),
                ("core_version", coreVersion),
                ("is_kana", isKana.ToString())
            );
            var url =
                $"{_baseUrl}/accent_phrases?{queryString}";

            return PostAsync<AccentPhrase[]>(url, ct);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public ValueTask<AccentPhrase[]> FetchMoraDataAsync(int speakerId,
            AccentPhrase[] accentPhrases,
            string? coreVersion = null,
            CancellationToken ct = default)
        {
            var queryString = CreateQueryString(
                ("speaker", speakerId.ToString()),
                ("core_version", coreVersion)
            );

            var url = $"{_baseUrl}/mora_data?{queryString}";
            return PostAsync<AccentPhrase[], AccentPhrase[]>(url, accentPhrases, ct);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public ValueTask<AccentPhrase[]> FetchMoraLengthAsync(int speakerId,
            AccentPhrase[] accentPhrases,
            string? coreVersion = null,
            CancellationToken ct = default)
        {
            var queryString = CreateQueryString(
                ("speaker", speakerId.ToString()),
                ("core_version", coreVersion)
            );

            var url = $"{_baseUrl}/mora_length?{queryString}";
            return PostAsync<AccentPhrase[], AccentPhrase[]>(url, accentPhrases, ct);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public ValueTask<AccentPhrase[]> FetchMoraPitchAsync(int speakerId,
            AccentPhrase[] accentPhrases,
            string? coreVersion = null,
            CancellationToken ct = default)
        {
            var queryString = CreateQueryString(
                ("speaker", speakerId.ToString()),
                ("core_version", coreVersion)
            );

            var url = $"{_baseUrl}/mora_pitch?{queryString}";
            return PostAsync<AccentPhrase[], AccentPhrase[]>(url, accentPhrases, ct);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public ValueTask<decimal[]> FetchSingFrameVolumeAsync(int speakerId,
            Score score,
            FrameAudioQuery frameAudioQuery,
            string? coreVersion = null,
            CancellationToken ct = default)
        {
            var request = new SingFrameVolumeRequest(score, frameAudioQuery);
            var queryString = CreateQueryString(
                ("speaker", speakerId.ToString()),
                ("core_version", coreVersion)
            );

            var url = $"{_baseUrl}/sing_frame_volume?{queryString}";
            return PostAsync<SingFrameVolumeRequest, decimal[]>(url, request, ct);
        }
    }
}