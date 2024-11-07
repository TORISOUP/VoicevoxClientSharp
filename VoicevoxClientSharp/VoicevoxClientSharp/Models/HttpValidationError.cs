using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace VoicevoxClientSharp.Models
{
    public readonly struct HttpValidationError : IEquatable<HttpValidationError>
    {
        public HttpValidationError(string json)
        {
            Json = json;
        }

        public string Json { get; }

        public bool Equals(HttpValidationError other)
        {
            return Json == other.Json;
        }

        public override bool Equals(object? obj)
        {
            return obj is HttpValidationError other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Json.GetHashCode();
        }
    }
}