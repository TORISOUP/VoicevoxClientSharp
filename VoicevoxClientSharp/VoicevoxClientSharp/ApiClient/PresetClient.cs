using System;
using System.Threading;
using System.Threading.Tasks;
using VoicevoxClientSharp.Models;

namespace VoicevoxClientSharp.ApiClient
{
    public interface IPresetClient : IDisposable
    {
        /// <summary>
        /// GET /presets
        /// エンジンが保持しているプリセットの設定を返します
        /// </summary>
        ValueTask<Preset[]> GetPresetsAsync(
            string? coreVersion = null,
            CancellationToken ct = default);

        /// <summary>
        /// POST /add_preset
        /// 新しいプリセットを追加します
        /// </summary>
        /// <param name="preset"></param>
        /// <param name="ct"></param>
        /// <returns>追加したプリセットのプリセットID</returns>
        ValueTask<int> AddPresetAsync(
            Preset preset,
            CancellationToken ct = default);

        /// <summary>
        /// POST /update_preset
        /// 既存のプリセットを更新します
        /// </summary>
        /// <param name="preset"></param>
        /// <param name="ct"></param>
        /// <returns>更新したプリセットのプリセットID</returns>
        ValueTask<int> UpdatePresetAsync(
            Preset preset,
            CancellationToken ct = default);

        /// <summary>
        /// POST /delete_preset
        /// 既存のプリセットを削除します
        /// </summary>
        /// <param name="id">削除するプリセットのプリセットID</param>
        /// <param name="ct"></param>
        /// <returns>更新したプリセットのプリセットID</returns>
        ValueTask DeletePresetAsync(
            int id,
            CancellationToken ct = default);
    }

    public partial class RawRawApiClient
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public ValueTask<Preset[]> GetPresetsAsync(string? coreVersion = null, CancellationToken ct = default)
        {
            var queryString = CreateQueryString(
                ("core_version", coreVersion)
            );

            var url = $"{_baseUrl}/presets?{queryString}";
            return GetAsync<Preset[]>(url, ct);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public ValueTask<int> AddPresetAsync(Preset preset, CancellationToken ct = default)
        {
            var url = $"{_baseUrl}/add_preset";
            return PostAsync<Preset, int>(url, preset, ct);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public ValueTask<int> UpdatePresetAsync(Preset preset, CancellationToken ct = default)
        {
            var url = $"{_baseUrl}/update_preset";
            return PostAsync<Preset, int>(url, preset, ct);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async ValueTask DeletePresetAsync(int id, CancellationToken ct = default)
        {
            using var lcts = CancellationTokenSource.CreateLinkedTokenSource(ct, _cts.Token);
            var ct2 = lcts.Token;
            var url = $"{_baseUrl}/delete_preset?id={id}";
            var response = await _httpClient.PostAsync(url, null, ct2);
            if ((int)response.StatusCode >= 400)
            {
                var errorJson = await response.Content.ReadAsStringAsync();
                throw new VoicevoxApiErrorException(errorJson, errorJson, (int)response.StatusCode);
            }
        }
    }
}