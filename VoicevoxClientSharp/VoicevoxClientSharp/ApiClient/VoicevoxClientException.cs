using System;
using VoicevoxClientSharp.Models;

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
        public string Json { get; }
        public int StatusCode { get; }

        public VoicevoxApiErrorException(string message, string json, int statusCode) : base(message)
        {
            Json = json;
            StatusCode = statusCode;
        }
    }
}