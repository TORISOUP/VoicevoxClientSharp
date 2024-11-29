using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using VoicevoxClientSharp.ApiClient.Models;

namespace VoicevoxClientSharp.ApiClient
{
    public interface IMiscClient : IDisposable
    {
        /// <summary>
        /// POST /connect_waves
        /// base64エンコードされたwavデータを一纏めにし、wavファイルで返します。
        /// </summary>
        ValueTask<byte[]> PostConnectWavesAsync(string[] waves, CancellationToken cancellationToken = default);

        /// <summary>
        /// POST /validate_kana
        /// テキストがAquesTalk 風記法に従っているかどうかを判定します。
        /// </summary>
        ValueTask<(bool isOk, ParseKanaBadRequest? error)> PostValidateKanaAsync(string text,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// GET /supported_devices
        /// 対応デバイスの一覧を取得します。
        /// </summary>
        ValueTask<SupportedDevicesInfo> GetSupportedDevicesAsync(
            string? coreVersion = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// GET /version
        /// エンジンのバージョンを取得します。
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        ValueTask<string> GetVersionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// GET /core_versions
        /// 利用可能なコアのバージョン一覧を取得します。
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        ValueTask<string[]> GetCoreVersionsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// GET /engine_manifest
        /// エンジンマニフェストを取得します。
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        ValueTask<EngineManifest> GetEngineManifestAsync(CancellationToken cancellationToken = default);
    }

    public partial class VoicevoxApiClient
    {
        /// <summary>
        ///     <inheritdoc />
        /// </summary>
        public ValueTask<byte[]> PostConnectWavesAsync(string[] waves, CancellationToken cancellationToken)
        {
            var url = $"{_baseUrl}/connect_waves";
            return PostAndByteResponseAsync(url, waves, cancellationToken);
        }

        /// <summary>
        ///     <inheritdoc />
        /// </summary>
        public async ValueTask<(bool isOk, ParseKanaBadRequest? error)> PostValidateKanaAsync(string text,
            CancellationToken cancellationToken)
        {
            var queryString = CreateQueryString(
                ("text", text)
            );
            var url = $"{_baseUrl}/validate_kana?{queryString}";

            try
            {
                var result = await PostAsync<bool>(url, cancellationToken);
                if (result)
                {
                    return (true, null);
                }

                // ここは本来到達しない
                throw new Exception("Unexpected error");
            }
            catch (VoicevoxApiErrorException ex)
            {
                // BadRequestなら専用の型にして返す
                if (ex.StatusCode == 400)
                {
                    var json = ex.Json;
                    var error = JsonSerializer.Deserialize<ParseKanaBadRequest>(json, _jsonSerializerOptions);
                    return (false, error);
                }

                throw;
            }
        }

        /// <summary>
        ///     <inheritdoc />
        /// </summary>
        public ValueTask<SupportedDevicesInfo> GetSupportedDevicesAsync(string? coreVersion = null,
            CancellationToken cancellationToken = default)
        {
            var queryString = CreateQueryString(
                ("core_version", coreVersion)
            );

            var url = $"{_baseUrl}/supported_devices?{queryString}";
            return GetAsync<SupportedDevicesInfo>(url, cancellationToken);
        }

        public async ValueTask<string> GetVersionAsync(CancellationToken cancellationToken = default)
        {
            var url = $"{_baseUrl}/version";
            using var lcts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, _cts.Token);
            var ct2 = lcts.Token;

            var response = await _httpClient.GetAsync(url, ct2);
            if ((int)response.StatusCode >= 400)
            {
                var errorJson = await response.Content.ReadAsStringAsync();
                throw new VoicevoxApiErrorException(errorJson, errorJson, (int)response.StatusCode);
            }

            return await response.Content.ReadAsStringAsync();
        }

        public ValueTask<string[]> GetCoreVersionsAsync(CancellationToken cancellationToken = default)
        {
            var url = $"{_baseUrl}/core_versions";
            return GetAsync<string[]>(url, cancellationToken);
        }

        public ValueTask<EngineManifest> GetEngineManifestAsync(CancellationToken cancellationToken = default)
        {
            var url = $"{_baseUrl}/engine_manifest";
            return GetAsync<EngineManifest>(url, cancellationToken);
        }
    }
}