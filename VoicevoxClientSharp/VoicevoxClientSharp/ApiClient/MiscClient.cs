using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using VoicevoxClientSharp.Models;

namespace VoicevoxClientSharp.ApiClient
{
    public interface IMiscClient
    {
        /// <summary>
        /// base64エンコードされたwavデータを一纏めにし、wavファイルで返します。
        /// </summary>
        ValueTask<byte[]> PostConnectWavesAsync(string[] waves, CancellationToken ct = default);

        /// <summary>
        /// テキストがAquesTalk 風記法に従っているかどうかを判定します。 
        /// </summary>
        ValueTask<(bool isOk, ParseKanaBadRequest? error)> PostValidateKanaAsync(string text,
            CancellationToken ct = default);
        
        /// <summary>
        /// 対応デバイスの一覧を取得します。
        /// </summary>
        ValueTask<SupportedDevicesInfo> GetSupportedDevicesAsync(
            string? coreVersion = null,
            CancellationToken ct = default);

    }

    public partial class RawApiClient
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public ValueTask<byte[]> PostConnectWavesAsync(string[] waves, CancellationToken ct)
        {
            var url = $"{_baseUrl}/connect_waves";
            return PostAndByteResponseAsync(url, waves, ct);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async ValueTask<(bool isOk, ParseKanaBadRequest? error)> PostValidateKanaAsync(string text,
            CancellationToken ct)
        {
            var queryString = QueryString(
                ("text", text)
            );
            var url = $"{_baseUrl}/validate_kana?{queryString}";

            try
            {
                var result = await PostAsync<bool>(url, ct);
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
        /// <inheritdoc/>
        /// </summary>
        public ValueTask<SupportedDevicesInfo> GetSupportedDevicesAsync(string? coreVersion = null,
            CancellationToken ct = default)
        {
            var queryString = QueryString(
                ("core_version", coreVersion)
            );

            var url = $"{_baseUrl}/supported_devices?{queryString}";
            return GetAsync<SupportedDevicesInfo>(url, ct);
        }
    }
    
    
}