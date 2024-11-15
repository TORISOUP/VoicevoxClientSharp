using System.Runtime.Serialization;

namespace VoicevoxClientSharp.Models
{
    /// <summary>
    /// 品詞
    /// </summary>
    /// <value>品詞</value>
    public enum WordTypes
    {
        /// <summary>
        /// Enum PROPERNOUN for value: PROPER_NOUN
        /// </summary>
        [EnumMember(Value = "PROPER_NOUN")] Propernoun = 1,

        /// <summary>
        /// Enum COMMONNOUN for value: COMMON_NOUN
        /// </summary>
        [EnumMember(Value = "COMMON_NOUN")] Commonnoun = 2,

        /// <summary>
        /// Enum VERB for value: VERB
        /// </summary>
        [EnumMember(Value = "VERB")] Verb = 3,

        /// <summary>
        /// Enum ADJECTIVE for value: ADJECTIVE
        /// </summary>
        [EnumMember(Value = "ADJECTIVE")] Adjective = 4,

        /// <summary>
        /// Enum SUFFIX for value: SUFFIX
        /// </summary>
        [EnumMember(Value = "SUFFIX")] Suffix = 5
    }
}