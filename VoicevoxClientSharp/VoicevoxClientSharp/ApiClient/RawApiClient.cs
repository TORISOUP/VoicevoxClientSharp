using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace VoicevoxClientSharp.ApiClient
{
    /// <summary>
    /// VOICEVOX APIのクライアント
    /// </summary>
    public interface IVoicevoxRawApiClient :
        IQueryClient,
        ISynthesisClient,
        IMiscClient,
        ISpeakerClient,
        IPresetClient,
        ILibraryClient,
        IUserDictionaryClient
    {
    }

    /// <summary>
    /// VOICEVOX APIのクライアント実装
    /// それぞれのREST APIと1:1対応
    /// </summary>
    public partial class VoicevoxRawApiClient : IVoicevoxRawApiClient
    {
        private readonly string _baseUrl = "http://localhost:50021";
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();


        // HttpClientを自前で生成した場合はtrue
        private readonly bool _handleHttpClient;
        private readonly HttpClient _httpClient;

        private readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        private bool IsDisposed { get; set; }

        public void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }

            IsDisposed = true;
            _cts.Cancel();
            _cts.Dispose();

            if (_handleHttpClient)
            {
                _httpClient.Dispose();
            }
        }

        #region Constructors

        public VoicevoxRawApiClient()
        {
            _httpClient = new HttpClient();
            _handleHttpClient = true;
        }

        public VoicevoxRawApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public VoicevoxRawApiClient(string baseUrl)
        {
            _baseUrl = baseUrl;
            _httpClient = new HttpClient();
            _handleHttpClient = true;
        }

        public VoicevoxRawApiClient(string baseUrl, HttpClient httpClient)
        {
            _baseUrl = baseUrl;
            _httpClient = httpClient;
        }

        #endregion

        #region HTTP

        internal async ValueTask<TResult> GetAsync<TResult>(string url, CancellationToken cancellationToken = default)
        {
            using var lcts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, _cts.Token);
            var ct = lcts.Token;
            var response = await _httpClient.GetAsync(url, ct);
            if ((int)response.StatusCode >= 400)
            {
                var errorJson = await response.Content.ReadAsStringAsync();
                throw new VoicevoxApiErrorException(errorJson, errorJson, (int)response.StatusCode);
            }

            var json = await response.Content.ReadAsStringAsync();
            if (json == null)
            {
                throw new VoicevoxClientException("Response was empty");
            }

            return JsonSerializer.Deserialize<TResult>(json)!;
        }

        internal async ValueTask PutAsync(
            string url,
            CancellationToken cancellationToken = default)
        {
            using var lcts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, _cts.Token);
            var ct = lcts.Token;
            var response = await _httpClient.PutAsync(url, null, ct);
            if ((int)response.StatusCode >= 400)
            {
                var errorJson = await response.Content.ReadAsStringAsync();
                throw new VoicevoxApiErrorException(errorJson, errorJson, (int)response.StatusCode);
            }
        }

        internal async ValueTask<TResult> PostAsync<TResult>(
            string url,
            CancellationToken cancellationToken = default)
        {
            using var lcts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, _cts.Token);
            var ct = lcts.Token;
            var response = await _httpClient.PostAsync(url, null, ct);
            if ((int)response.StatusCode >= 400)
            {
                var errorJson = await response.Content.ReadAsStringAsync();
                throw new VoicevoxApiErrorException(errorJson, errorJson, (int)response.StatusCode);
            }

            var responseJson = await response.Content.ReadAsStringAsync();
            if (responseJson == null)
            {
                throw new VoicevoxClientException("Response was empty");
            }

            return JsonSerializer.Deserialize<TResult>(responseJson)!;
        }

        internal async ValueTask<TResult> PostAsync<TRequest, TResult>(
            string url,
            TRequest request,
            CancellationToken cancellationToken = default)
        {
            using var lcts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, _cts.Token);
            var ct = lcts.Token;
            var requestJson = JsonSerializer.Serialize(request, _jsonSerializerOptions);
            var content = new StringContent(requestJson, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, content, ct);
            if ((int)response.StatusCode >= 400)
            {
                var errorJson = await response.Content.ReadAsStringAsync();
                throw new VoicevoxApiErrorException(errorJson, errorJson, (int)response.StatusCode);
            }

            var responseJson = await response.Content.ReadAsStringAsync();
            if (responseJson == null)
            {
                throw new VoicevoxClientException("Response was empty");
            }

            return JsonSerializer.Deserialize<TResult>(responseJson)!;
        }

        internal async ValueTask<byte[]> PostAndByteResponseAsync<TRequest>(
            string url,
            TRequest request,
            CancellationToken cancellationToken = default)
        {
            using var lcts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, _cts.Token);
            var ct = lcts.Token;
            var requestJson = JsonSerializer.Serialize(request, _jsonSerializerOptions);
            var content = new StringContent(requestJson, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, content, ct);
            if ((int)response.StatusCode >= 400)
            {
                var errorJson = await response.Content.ReadAsStringAsync();
                throw new VoicevoxApiErrorException(errorJson, errorJson, (int)response.StatusCode);
            }

            return await response.Content.ReadAsByteArrayAsync();
        }

        internal string CreateQueryString(params (string key, string? value)[] query)
        {
            var sb = new StringBuilder();
            foreach (var (key, value) in query)
            {
                if (value == null)
                {
                    continue;
                }

                if (sb.Length > 0)
                {
                    sb.Append("&");
                }

                sb.Append($"{key}={Uri.EscapeDataString(value)}");
            }

            return sb.ToString();
        }

        #endregion
    }
}