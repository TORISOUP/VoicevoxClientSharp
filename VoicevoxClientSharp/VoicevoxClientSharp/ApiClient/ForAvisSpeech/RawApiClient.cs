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
            CancellationToken cancellationToken)
        {
            var queryString = CreateQueryString(
                ("text", text),
                ("speaker", speakerId.ToString()),
                ("core_version", coreVersion)
            );
            var url = $"{_baseUrl}/audio_query?{queryString}";
            return PostAsync<AvisSpeechAudioQuery>(url, cancellationToken);
        }

        ValueTask<AvisSpeechAudioQuery> IQueryClient<AvisSpeechAudioQuery>.CreateAudioQueryFromPresetAsync(string text,
            int presetId,
            string? coreVersion,
            CancellationToken cancellationToken)
        {
            var queryString = CreateQueryString(
                ("text", text),
                ("preset_id", presetId.ToString()),
                ("core_version", coreVersion)
            );
            var url =
                $"{_baseUrl}/audio_query_from_preset?{queryString}";
            return PostAsync<AvisSpeechAudioQuery>(url, cancellationToken);
        }
        
        
        ValueTask<byte[]> ISynthesisClient<AvisSpeechAudioQuery>.SynthesisAsync(int speakerId,
            AvisSpeechAudioQuery audioQuery,
            bool? enableInterrogativeUpspeak,
            string? coreVersion,
            CancellationToken cancellationToken)
        {
            var queryString = CreateQueryString(
                ("speaker", speakerId.ToString()),
                ("core_version", coreVersion),
                ("enable_interrogative_upspeak", enableInterrogativeUpspeak?.ToString())
            );
            var url = $"{_baseUrl}/synthesis?{queryString}";
            return PostAndByteResponseAsync(url, audioQuery, cancellationToken);
        }

        [Obsolete("This method is not implemented in AvisSpeech.")]
        ValueTask<byte[]> ISynthesisClient<AvisSpeechAudioQuery>.CancellableSynthesisAsync(int speakerId,
            AvisSpeechAudioQuery audioQuery,
            bool? enableInterrogativeUpspeak,
            string? coreVersion,
            CancellationToken cancellationToken)
        {
            // 未対応だが定義上は残している
            throw new NotImplementedException();
        }

        ValueTask<byte[]> ISynthesisClient<AvisSpeechAudioQuery>.MultiSpeakerSynthesisAsync(int speakerId,
            AvisSpeechAudioQuery[] audioQueries,
            string? coreVersion,
            CancellationToken cancellationToken)
        {
            var queryString = CreateQueryString(
                ("speaker", speakerId.ToString()),
                ("core_version", coreVersion)
            );
            var url = $"{_baseUrl}/multi_synthesis?{queryString}";
            return PostAndByteResponseAsync(url, audioQueries, cancellationToken);
        }
    }
}