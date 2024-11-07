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

        ValueTask<AudioQuery> PostAudioQueryFromPresetAsync(string text,
            int presetId,
            string? coreVersion = "",
            CancellationToken ct = default);

        ValueTask<FrameAudioQuery> PostSingFrameAudioQueryAsync(Notes notes,
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

        public ValueTask<AudioQuery> PostAudioQueryFromPresetAsync(string text,
            int presetId,
            string? coreVersion = "",
            CancellationToken ct = default)
        {
            var escapedText = Uri.EscapeDataString(text);
            var url =
                $"{_baseUrl}/audio_query_from_preset?text={escapedText}&preset_id={presetId}&core_version={coreVersion}";
            return PostAsync<AudioQuery>(url, ct);
        }

        public ValueTask<FrameAudioQuery> PostSingFrameAudioQueryAsync(Notes notes,
            int speakerId,
            string? coreVersion = "",
            CancellationToken ct = default)
        {
            var url = $"{_baseUrl}/sing_frame_audio_query?speaker={speakerId}&core_version={coreVersion}";
            return PostAsync<FrameAudioQuery, Notes>(url, notes, ct);
        }
    }
}