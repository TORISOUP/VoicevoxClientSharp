using System;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace VoicevoxClientSharp.ApiClient.Models
{
    /// <summary>
    /// MorphableTargetInfo
    /// </summary>
    [DataContract(Name = "MorphableTargetInfo")]
    public sealed class MorphableTargetInfo : IEquatable<MorphableTargetInfo>
    {
        [JsonConstructor]
        public MorphableTargetInfo()
        {
        }

        /// <summary>
        /// 指定したキャラクターに対してモーフィングの可否
        /// </summary>
        /// <value>指定したキャラクターに対してモーフィングの可否</value>
        [JsonPropertyName("is_morphable")]
        public bool IsMorphable { get; set; }

        /// <summary>
        /// Returns true if MorphableTargetInfo instances are equal
        /// </summary>
        /// <param name="input">Instance of MorphableTargetInfo to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(MorphableTargetInfo? input)
        {
            if (input == null)
            {
                return false;
            }

            return
                IsMorphable == input.IsMorphable ||
                IsMorphable.Equals(input.IsMorphable);
        }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class MorphableTargetInfo {\n");
            sb.Append("  IsMorphable: ").Append(IsMorphable).Append("\n");
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
            return Equals(input as MorphableTargetInfo);
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
                hashCode = hashCode * 59 + IsMorphable.GetHashCode();
                return hashCode;
            }
        }
    }
}