using System;
using System.Threading;
using System.Threading.Tasks;
using VoicevoxClientSharp.Models;

namespace VoicevoxClientSharp.ApiClient
{
    public interface IQueryClient : IDisposable
    {
        ValueTask<AudioQuery> PostAudioQueryAsync(string text,
            int speakerId,
            string? coreVersion = "",
            CancellationToken ct = default);
    }

    public partial class RawApiClient
    {
        public ValueTask<AudioQuery> PostAudioQueryAsync(string text,
            int speakerId,
            string? coreVersion = "",
            CancellationToken ct = default)
        {
            var escapedText = Uri.EscapeDataString(text);
            var url = $"{_baseUrl}/audio_query?text={escapedText}&speaker={speakerId}&core_version={coreVersion}";
            return PostAsync<AudioQuery>(url, ct);
        }
    }
}