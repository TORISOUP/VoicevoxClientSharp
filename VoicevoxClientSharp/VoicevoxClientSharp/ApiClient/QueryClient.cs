using System;
using System.Threading;
using System.Threading.Tasks;
using VoicevoxClientSharp.Models;

namespace VoicevoxClientSharp.ApiClient
{
    public interface IQueryClient : IDisposable
    {
        /// <summary>
        /// 音声合成用のクエリを作成する
        /// </summary>
        ValueTask<AudioQuery> PostAudioQueryAsync(string text,
            int speakerId,
            string? coreVersion = "",
            CancellationToken ct = default);

        /// <summary>
        /// 音声合成用のクエリをプリセットを用いて作成する
        /// </summary>
        ValueTask<AudioQuery> PostAudioQueryFromPresetAsync(string text,
            int presetId,
            string? coreVersion = "",
            CancellationToken ct = default);

        /// <summary>
        /// 歌唱音声合成用のクエリを作成する
        /// </summary>
        ValueTask<FrameAudioQuery> PostSingFrameAudioQueryAsync(Notes notes,
            int speakerId,
            string? coreVersion = "",
            CancellationToken ct = default);

        /// <summary>
        /// テキストからアクセント句を得る
        /// </summary>
        ValueTask<AccentPhrase[]> PostAccentPhraseAsync(string text,
            int speakerId,
            bool? isKana = false,
            string? coreVersion = "",
            CancellationToken ct = default);

        /// <summary>
        /// アクセント句から音高・音素長を得る
        /// </summary>
        ValueTask<AccentPhrase[]> PostMoraDataAsync(
            int speakerId,
            AccentPhrase[] accentPhrases,
            string? coreVersion = "",
            CancellationToken ct = default);

        /// <summary>
        /// アクセント句から音素長を得る
        /// </summary>
        ValueTask<AccentPhrase[]> PostMoraLengthAsync(
            int speakerId,
            AccentPhrase[] accentPhrases,
            string? coreVersion = "",
            CancellationToken ct = default);

        /// <summary>
        /// アクセント句から音高を得る
        /// </summary>
        ValueTask<AccentPhrase[]> PostMoraPitchAsync(
            int speakerId,
            AccentPhrase[] accentPhrases,
            string? coreVersion = "",
            CancellationToken ct = default);

        /// <summary>
        /// 楽譜・歌唱音声合成用のクエリからフレームごとの音量を得る
        /// </summary>
        ValueTask<AccentPhrase[]> PostSingFrameVolumeAsync(
            int speakerId,
            AccentPhrase[] accentPhrases,
            string? coreVersion = "",
            CancellationToken ct = default);
    }

    public partial class RawApiClient
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public ValueTask<AudioQuery> PostAudioQueryAsync(string text,
            int speakerId,
            string? coreVersion = "",
            CancellationToken ct = default)
        {
            var queryString = QueryString(
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
        public ValueTask<AudioQuery> PostAudioQueryFromPresetAsync(string text,
            int presetId,
            string? coreVersion = "",
            CancellationToken ct = default)
        {
            var queryString = QueryString(
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
        public ValueTask<FrameAudioQuery> PostSingFrameAudioQueryAsync(Notes notes,
            int speakerId,
            string? coreVersion = "",
            CancellationToken ct = default)
        {
            var queryString = QueryString(
                ("speaker", speakerId.ToString()),
                ("core_version", coreVersion)
            );

            var url = $"{_baseUrl}/sing_frame_audio_query?{queryString}";
            return PostAsync<FrameAudioQuery, Notes>(url, notes, ct);
        }

        public ValueTask<AccentPhrase[]> PostAccentPhraseAsync(string text,
            int speakerId,
            bool? isKana,
            string? coreVersion = "",
            CancellationToken ct = default)
        {
            var queryString = QueryString(
                ("text", text),
                ("speaker", speakerId.ToString()),
                ("core_version", coreVersion),
                ("is_kana", isKana.ToString())
            );
            var url =
                $"{_baseUrl}/accent_phrases?{queryString}";

            return PostAsync<AccentPhrase[]>(url, ct);
        }

        public ValueTask<AccentPhrase[]> PostMoraDataAsync(int speakerId,
            AccentPhrase[] accentPhrases,
            string? coreVersion = "",
            CancellationToken ct = default)
        {
            var queryString = QueryString(
                ("speaker", speakerId.ToString()),
                ("core_version", coreVersion)
            );
            
            var url = $"{_baseUrl}/mora_data?{queryString}";
            return PostAsync<AccentPhrase[], AccentPhrase[]>(url, accentPhrases, ct);
        }

        public ValueTask<AccentPhrase[]> PostMoraLengthAsync(int speakerId,
            AccentPhrase[] accentPhrases,
            string? coreVersion = "",
            CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public ValueTask<AccentPhrase[]> PostMoraPitchAsync(int speakerId,
            AccentPhrase[] accentPhrases,
            string? coreVersion = "",
            CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public ValueTask<AccentPhrase[]> PostSingFrameVolumeAsync(int speakerId,
            AccentPhrase[] accentPhrases,
            string? coreVersion = "",
            CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
}