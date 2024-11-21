using System;
using System.Threading;
using System.Threading.Tasks;
using VoicevoxClientSharp.ApiClient.ForAvisSpeech;

namespace VoicevoxClientSharp.ApiClient
{
    /// <summary>
    /// AvisSpeech APIのクライアント実装
    /// </summary>
    public partial class VoicevoxApiClient
    {
        ValueTask<AvisSpeechAudioQuery> IQueryClient<AvisSpeechAudioQuery>.CreateAudioQueryAsync(string text,
            int speakerId,
            string? coreVersion,
            CancellationToken ct)
        {
            var queryString = CreateQueryString(
                ("text", text),
                ("speaker", speakerId.ToString()),
                ("core_version", coreVersion)
            );
            var url = $"{_baseUrl}/audio_query?{queryString}";
            return PostAsync<AvisSpeechAudioQuery>(url, ct);
        }

        ValueTask<AvisSpeechAudioQuery> IQueryClient<AvisSpeechAudioQuery>.CreateAudioQueryFromPresetAsync(string text,
            int presetId,
            string? coreVersion,
            CancellationToken ct)
        {
            var queryString = CreateQueryString(
                ("text", text),
                ("preset_id", presetId.ToString()),
                ("core_version", coreVersion)
            );
            var url =
                $"{_baseUrl}/audio_query_from_preset?{queryString}";
            return PostAsync<AvisSpeechAudioQuery>(url, ct);
        }
        
        
        ValueTask<byte[]> ISynthesisClient<AvisSpeechAudioQuery>.SynthesisAsync(int speakerId,
            AvisSpeechAudioQuery audioQuery,
            bool? enableInterrogativeUpspeak,
            string? coreVersion,
            CancellationToken ct)
        {
            var queryString = CreateQueryString(
                ("speaker", speakerId.ToString()),
                ("core_version", coreVersion),
                ("enable_interrogative_upspeak", enableInterrogativeUpspeak?.ToString())
            );
            var url = $"{_baseUrl}/synthesis?{queryString}";
            return PostAndByteResponseAsync(url, audioQuery, ct);
        }

        [Obsolete("This method is not implemented in AvisSpeech.")]
        ValueTask<byte[]> ISynthesisClient<AvisSpeechAudioQuery>.CancellableSynthesisAsync(int speakerId,
            AvisSpeechAudioQuery audioQuery,
            bool? enableInterrogativeUpspeak,
            string? coreVersion,
            CancellationToken ct)
        {
            // 未対応だが定義上は残している
            throw new NotImplementedException();
        }

        ValueTask<byte[]> ISynthesisClient<AvisSpeechAudioQuery>.MultiSpeakerSynthesisAsync(int speakerId,
            AvisSpeechAudioQuery[] audioQueries,
            string? coreVersion,
            CancellationToken ct)
        {
            var queryString = CreateQueryString(
                ("speaker", speakerId.ToString()),
                ("core_version", coreVersion)
            );
            var url = $"{_baseUrl}/multi_synthesis?{queryString}";
            return PostAndByteResponseAsync(url, audioQueries, ct);
        }
    }
}