using System;
using System.Runtime.Serialization;
using System.Text;

namespace VoicevoxClientSharp.ApiClient.Models
{
    /// <summary>
    /// キャラクターの対応機能の情報
    /// </summary>
    [DataContract(Name = "SpeakerSupportedFeatures")]
    public sealed class SpeakerSupportedFeatures : IEquatable<SpeakerSupportedFeatures>
    {
        /// <summary>
        /// モーフィング機能への対応。&#39;ALL&#39; は「全て許可」、&#39;SELF_ONLY&#39; は「同じキャラクター内でのみ許可」、&#39;NOTHING&#39; は「全て禁止」
        /// </summary>
        /// <value>モーフィング機能への対応。&#39;ALL&#39; は「全て許可」、&#39;SELF_ONLY&#39; は「同じキャラクター内でのみ許可」、&#39;NOTHING&#39; は「全て禁止」</value>
        public enum PermittedSynthesisMorphingEnum
        {
            /// <summary>
            /// Enum ALL for value: ALL
            /// </summary>
            [EnumMember(Value = "ALL")] ALL = 1,

            /// <summary>
            /// Enum SELFONLY for value: SELF_ONLY
            /// </summary>
            [EnumMember(Value = "SELF_ONLY")] SELFONLY = 2,

            /// <summary>
            /// Enum NOTHING for value: NOTHING
            /// </summary>
            [EnumMember(Value = "NOTHING")] NOTHING = 3
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SpeakerSupportedFeatures" /> class.
        /// </summary>
        /// <param name="permittedSynthesisMorphing">
        /// モーフィング機能への対応。&#39;ALL&#39; は「全て許可」、&#39;SELF_ONLY&#39; は「同じキャラクター内でのみ許可」、&#39;
        /// NOTHING&#39; は「全て禁止」 (default to PermittedSynthesisMorphingEnum.ALL).
        /// </param>
        public SpeakerSupportedFeatures(PermittedSynthesisMorphingEnum? permittedSynthesisMorphing =
            PermittedSynthesisMorphingEnum.ALL)
        {
            PermittedSynthesisMorphing = permittedSynthesisMorphing;
        }


        /// <summary>
        /// モーフィング機能への対応。&#39;ALL&#39; は「全て許可」、&#39;SELF_ONLY&#39; は「同じキャラクター内でのみ許可」、&#39;NOTHING&#39; は「全て禁止」
        /// </summary>
        /// <value>モーフィング機能への対応。&#39;ALL&#39; は「全て許可」、&#39;SELF_ONLY&#39; は「同じキャラクター内でのみ許可」、&#39;NOTHING&#39; は「全て禁止」</value>
        [DataMember(Name = "permitted_synthesis_morphing", EmitDefaultValue = false)]
        public PermittedSynthesisMorphingEnum? PermittedSynthesisMorphing { get; set; }

        /// <summary>
        /// Returns true if SpeakerSupportedFeatures instances are equal
        /// </summary>
        /// <param name="input">Instance of SpeakerSupportedFeatures to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(SpeakerSupportedFeatures? input)
        {
            if (input == null)
            {
                return false;
            }

            return
                PermittedSynthesisMorphing == input.PermittedSynthesisMorphing ||
                PermittedSynthesisMorphing.Equals(input.PermittedSynthesisMorphing);
        }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class SpeakerSupportedFeatures {\n");
            sb.Append("  PermittedSynthesisMorphing: ").Append(PermittedSynthesisMorphing).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }


        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object? input)
        {
            return Equals(input as SpeakerSupportedFeatures);
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                var hashCode = 41;
                hashCode = hashCode * 59 + PermittedSynthesisMorphing.GetHashCode();
                return hashCode;
            }
        }
    }
}