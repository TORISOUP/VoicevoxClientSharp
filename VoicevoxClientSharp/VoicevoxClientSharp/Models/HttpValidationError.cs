using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace VoicevoxClientSharp.Models
{
    public sealed class HttpValidationError : IEquatable<HttpValidationError>
    {
        [DataMember(Name = "detail")]
        public Detail[] Detail { get; set; }

        public HttpValidationError(Detail[] detail)
        {
            Detail = detail;
        }

        public bool Equals(HttpValidationError? other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Detail.Equals(other.Detail);
        }

        public override bool Equals(object? obj)
        {
            return ReferenceEquals(this, obj) || obj is HttpValidationError other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Detail.GetHashCode();
        }
    }
    
    [Serializable]
    public sealed class Detail : IEquatable<Detail>
    {
        public Detail(List<object> loc, string msg, string type, object input)
        {
            Loc = loc;
            Msg = msg;
            Type = type;
            Input = input;
        }

        [DataMember(Name = "loc")] public List<object> Loc { get; set; }
        [DataMember(Name = "msg")] public string Msg { get; set; }
        [DataMember(Name = "type")] public string Type { get; set; }
        [DataMember(Name = "input")] public object Input { get; set; }

        public bool Equals(Detail? other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Loc.Equals(other.Loc) && Msg == other.Msg && Type == other.Type && Input.Equals(other.Input);
        }

        public override bool Equals(object? obj)
        {
            return ReferenceEquals(this, obj) || obj is Detail other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Loc.GetHashCode();
                hashCode = (hashCode * 397) ^ Msg.GetHashCode();
                hashCode = (hashCode * 397) ^ Type.GetHashCode();
                hashCode = (hashCode * 397) ^ Input.GetHashCode();
                return hashCode;
            }
        }
    }
}