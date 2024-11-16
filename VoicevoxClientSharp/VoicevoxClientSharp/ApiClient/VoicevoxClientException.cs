using System;

namespace VoicevoxClientSharp.ApiClient
{
    public class VoicevoxClientException : Exception
    {
        public VoicevoxClientException(string message) : base(message)
        {
        }
    }

    public class VoicevoxApiErrorException : VoicevoxClientException
    {
        public VoicevoxApiErrorException(string message, string json, int statusCode) : base(message)
        {
            Json = json;
            StatusCode = statusCode;
        }

        public string Json { get; }
        public int StatusCode { get; }
    }
}