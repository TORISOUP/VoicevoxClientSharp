using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace VoicevoxClientSharp.Models
{
    /// <summary>
    /// CORSの許可モード
    /// </summary>
    /// <value>CORSの許可モード</value>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum CorsPolicyMode
    {
        /// <summary>
        /// Enum All for value: all
        /// </summary>
        [EnumMember(Value = "all")] All = 1,

        /// <summary>
        /// Enum Localapps for value: localapps
        /// </summary>
        [EnumMember(Value = "localapps")] Localapps = 2
    }
}