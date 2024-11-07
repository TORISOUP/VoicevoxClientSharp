using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace VoicevoxClientSharp.Models
{
    /// <summary>
    /// 品詞
    /// </summary>
    /// <value>品詞</value>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum WordTypes
    {
        /// <summary>
        /// Enum PROPERNOUN for value: PROPER_NOUN
        /// </summary>
        [EnumMember(Value = "PROPER_NOUN")] PROPERNOUN = 1,

        /// <summary>
        /// Enum COMMONNOUN for value: COMMON_NOUN
        /// </summary>
        [EnumMember(Value = "COMMON_NOUN")] COMMONNOUN = 2,

        /// <summary>
        /// Enum VERB for value: VERB
        /// </summary>
        [EnumMember(Value = "VERB")] VERB = 3,

        /// <summary>
        /// Enum ADJECTIVE for value: ADJECTIVE
        /// </summary>
        [EnumMember(Value = "ADJECTIVE")] ADJECTIVE = 4,

        /// <summary>
        /// Enum SUFFIX for value: SUFFIX
        /// </summary>
        [EnumMember(Value = "SUFFIX")] SUFFIX = 5
    }
}