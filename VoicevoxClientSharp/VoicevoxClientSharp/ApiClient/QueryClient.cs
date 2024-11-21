using System;
using System.Threading;
using System.Threading.Tasks;
using VoicevoxClientSharp.ApiClient.Models;

namespace VoicevoxClientSharp.ApiClient
{
    public interface IQueryClient<T> : IDisposable
    {
        /// <summary>
        /// POST /audio_query
        /// 音声合成用のクエリを作成する
        /// </summary>
        ValueTask<T> CreateAudioQueryAsync(string text,
            int speakerId,
            string? coreVersion = null,
            CancellationToken ct = default);

        /// <summary>
        /// POST /audio_query_from_preset
        /// 音声合成用のクエリをプリセットを用いて作成する
        /// </summary>
        ValueTask<T> CreateAudioQueryFromPresetAsync(string text,
            int presetId,
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
    }

    public partial class VoicevoxApiClient
    {
        /// <summary>
        ///     <inheritdoc />
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
        ///     <inheritdoc />
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
        ///     <inheritdoc />
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
        ///     <inheritdoc />
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
        ///     <inheritdoc />
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
        ///     <inheritdoc />
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
    }
}