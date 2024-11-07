using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VoicevoxClientSharp.Models;

namespace VoicevoxClientSharp.ApiClient
{
    /// <summary>
    /// Raw APIクライアント
    /// </summary>
    public partial class RawApiClient : IDisposable, IQueryClient
    {
        private readonly string _baseUrl = "http://localhost:50021";
        private readonly HttpClient _httpClient;

        // HttpClientを自前で生成した場合はtrue
        private readonly bool _handleHttpClient = false;
        private bool IsDisposed { get; set; }

        #region Constructors

        public RawApiClient()
        {
            _httpClient = new HttpClient();
            _handleHttpClient = true;
        }

        public RawApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public RawApiClient(string baseUrl)
        {
            _baseUrl = baseUrl;
            _httpClient = new HttpClient();
            _handleHttpClient = true;
        }

        public RawApiClient(string baseUrl, HttpClient httpClient)
        {
            _baseUrl = baseUrl;
            _httpClient = httpClient;
        }

        #endregion

        #region HTTP

        internal async ValueTask<TResult> GetAsync<TResult>(string url, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.GetAsync(url, cancellationToken);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            if (json == null) throw new VoicevoxClientException("Response was empty");
            return JsonConvert.DeserializeObject<TResult>(json!)!;
        }

        internal async ValueTask<TResult> PostAsync<TResult>(
            string url,
            CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsync(url, null, cancellationToken);
            if ((int)response.StatusCode == 422)
            {
                var errorJson = await response.Content.ReadAsStringAsync();
                var error = JsonConvert.DeserializeObject<HttpValidationError>(errorJson);
                throw new VoicevoxHttpValidationErrorException("HTTP Validation Error", error!);
            }

            var responseJson = await response.Content.ReadAsStringAsync();
            if (responseJson == null) throw new VoicevoxClientException("Response was empty");
            return JsonConvert.DeserializeObject<TResult>(responseJson)!;
        }

        #endregion

        public void Dispose()
        {
            if (IsDisposed) return;
            IsDisposed = true;

            if (_handleHttpClient)
            {
                _httpClient.Dispose();
            }
        }
    }
}