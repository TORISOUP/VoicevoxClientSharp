using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using VoicevoxClientSharp.ApiClient.Models;

namespace VoicevoxClientSharp.ApiClient
{
    public interface ISpeakerClient : IDisposable
    {
        /// <summary>
        /// POST /initialize_speaker
        /// 指定されたスタイルを初期化します。 実行しなくても他のAPIは使用できますが、初回実行時に時間がかかることがあります。
        /// </summary>
        /// <param name="skipReinit"></param>
        /// <param name="speaker">既に初期化済みのスタイルの再初期化をスキップするかどうか</param>
        /// <param name="coreVersion"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        ValueTask InitializeSpeakerAsync(int speaker,
            bool? skipReinit = false,
            string? coreVersion = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// GET /is_initialized_speaker
        /// 指定されたスタイルが初期化されているかどうかを返します。
        /// </summary>
        ValueTask<bool> IsInitializedSpeakerAsync(int speaker,
            string? coreVersion = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// GET /speakers
        /// 喋れるキャラクターの情報の一覧を返します。
        /// </summary>
        /// <param name="coreVersion"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        ValueTask<Speaker[]> GetSpeakersAsync(string? coreVersion = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// GET /speaker_info
        /// UUID で指定された喋れるキャラクターの情報を返します。 画像や音声はresourceFormatで指定した形式で返されます。
        /// </summary>
        /// <param name="speakerUuId"></param>
        /// <param name="resourceFormat"></param>
        /// <param name="coreVersion"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        ValueTask<SpeakerInfo> GetSpeakerInfoAsync(
            string speakerUuId,
            ResourceFormat? resourceFormat = ResourceFormat.Base64,
            string? coreVersion = null,
            CancellationToken cancellationToken = default);
    }

    public partial class VoicevoxApiClient
    {
        /// <summary>
        ///     <inheritdoc />
        /// </summary>
        public async ValueTask InitializeSpeakerAsync(int speaker,
            bool? skipReinit,
            string? coreVersion = null,
            CancellationToken cancellationToken = default)
        {
            var queryString = CreateQueryString(
                ("speaker", speaker.ToString()),
                ("skip_reinit", skipReinit?.ToString()),
                ("core_version", coreVersion)
            );

            var url = $"{_baseUrl}/initialize_speaker?{queryString}";

            var response = await _httpClient.PostAsync(url, null, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                return;
            }

            var errorJson = await response.Content.ReadAsStringAsync();
            throw new VoicevoxApiErrorException(errorJson, errorJson, (int)response.StatusCode);
        }

        /// <summary>
        ///     <inheritdoc />
        /// </summary>
        public async ValueTask<bool> IsInitializedSpeakerAsync(int speaker,
            string? coreVersion = null,
            CancellationToken cancellationToken = default)
        {
            var queryString = CreateQueryString(
                ("speaker", speaker.ToString()),
                ("core_version", coreVersion)
            );

            var url = $"{_baseUrl}/is_initialized_speaker?{queryString}";
            var response = await _httpClient.GetAsync(url, cancellationToken);
            if ((int)response.StatusCode >= 400)
            {
                var errorJson = await response.Content.ReadAsStringAsync();
                throw new VoicevoxApiErrorException(errorJson, errorJson, (int)response.StatusCode);
            }

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<bool>(json, _jsonSerializerOptions)!;
        }

        /// <summary>
        ///     <inheritdoc />
        /// </summary>
        public ValueTask<Speaker[]> GetSpeakersAsync(string? coreVersion = null, CancellationToken cancellationToken = default)
        {
            var queryString = CreateQueryString(
                ("core_version", coreVersion)
            );

            var url = $"{_baseUrl}/speakers?{queryString}";
            return GetAsync<Speaker[]>(url, cancellationToken);
        }

        /// <summary>
        ///     <inheritdoc />
        /// </summary>
        public ValueTask<SpeakerInfo> GetSpeakerInfoAsync(
            string speakerUuId,
            ResourceFormat? resourceFormat = ResourceFormat.Base64,
            string? coreVersion = null,
            CancellationToken cancellationToken = default)
        {
            var resourceFormatStr = resourceFormat switch
            {
                ResourceFormat.Base64 => "base64",
                ResourceFormat.Url => "url",
                null => "base64",
                _ => throw new ArgumentOutOfRangeException(nameof(resourceFormat), resourceFormat, null)
            };

            var queryString = CreateQueryString(
                ("speaker_uuid", speakerUuId),
                ("resource_format", resourceFormatStr),
                ("core_version", coreVersion)
            );

            var url = $"{_baseUrl}/speaker_info?{queryString}";
            return GetAsync<SpeakerInfo>(url, cancellationToken);
        }

    }
}