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

    public class VoicevoxHttpValidationErrorException : VoicevoxClientException
    {
        public HttpValidationError Error { get; }

        public VoicevoxHttpValidationErrorException(string message, HttpValidationError error) : base(message)
        {
            Error = error;
        }
    }
}