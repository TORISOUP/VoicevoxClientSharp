using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using VoicevoxClientSharp.Models;

namespace VoicevoxClientSharp.ApiClient
{
    public interface ISpeakerClient
    {
        /// <summary>
        /// 指定されたスタイルを初期化します。 実行しなくても他のAPIは使用できますが、初回実行時に時間がかかることがあります。
        /// </summary>
        /// <param name="skipReinit"></param>
        /// <param name="speaker">既に初期化済みのスタイルの再初期化をスキップするかどうか</param>
        /// <param name="coreVersion"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        ValueTask InitializeSpeakerAsync(int speaker,
            bool? skipReinit = false,
            string? coreVersion = null,
            CancellationToken ct = default);

        /// <summary>
        /// 指定されたスタイルが初期化されているかどうかを返します。
        /// </summary>
        ValueTask<bool> IsInitializedSpeakerAsync(int speaker,
            string? coreVersion = null,
            CancellationToken ct = default);


        /// <summary>
        /// GET /speakers
        /// 喋れるキャラクターの情報の一覧を返します。
        /// </summary>
        /// <param name="coreVersion"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        ValueTask<Speaker[]> GetSpeakersAsync(string? coreVersion = null, CancellationToken ct = default);
    }

    public partial class RawApiClient
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async ValueTask InitializeSpeakerAsync(int speaker,
            bool? skipReinit,
            string? coreVersion = null,
            CancellationToken ct = default)
        {
            var queryString = QueryString(
                ("speaker", speaker.ToString()),
                ("skip_reinit", skipReinit?.ToString()),
                ("core_version", coreVersion)
            );

            var url = $"{_baseUrl}/initialize_speaker?{queryString}";

            var response = await _httpClient.PostAsync(url, null, ct);

            if (response.IsSuccessStatusCode) return;

            var errorJson = await response.Content.ReadAsStringAsync();
            throw new VoicevoxApiErrorException(errorJson, errorJson, (int)response.StatusCode);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async ValueTask<bool> IsInitializedSpeakerAsync(int speaker,
            string? coreVersion = null,
            CancellationToken ct = default)
        {
            var queryString = QueryString(
                ("speaker", speaker.ToString()),
                ("core_version", coreVersion)
            );

            var url = $"{_baseUrl}/is_initialized_speaker?{queryString}";
            var response = await _httpClient.GetAsync(url, ct);
            if ((int)response.StatusCode >= 400)
            {
                var errorJson = await response.Content.ReadAsStringAsync();
                throw new VoicevoxApiErrorException(errorJson, errorJson, (int)response.StatusCode);
            }

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<bool>(json, _jsonSerializerOptions)!;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public ValueTask<Speaker[]> GetSpeakersAsync(string? coreVersion = null, CancellationToken ct = default)
        {
            var queryString = QueryString(
                ("core_version", coreVersion)
            );

            var url = $"{_baseUrl}/speakers?{queryString}";
            return GetAsync<Speaker[]>(url, ct);
        }
    }
}