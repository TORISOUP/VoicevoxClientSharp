using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;


namespace VoicevoxClientSharp.Models
{
    /// <summary>
    /// エンジンのアップデート情報
    /// </summary>
    [DataContract(Name = "UpdateInfo")]
    public sealed class UpdateInfo : IEquatable<UpdateInfo>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateInfo" /> class.
        /// </summary>
        /// <param name="varVersion">エンジンのバージョン名 (required).</param>
        /// <param name="descriptions">descriptions (required).</param>
        /// <param name="contributors">contributors.</param>
        public UpdateInfo(string varVersion,
            List<string> descriptions,
            List<string>? contributors)
        {
            VarVersion = varVersion;
            Descriptions = descriptions;
            Contributors = contributors;
        }

        /// <summary>
        /// エンジンのバージョン名
        /// </summary>
        /// <value>エンジンのバージョン名</value>
        [DataMember(Name = "version", IsRequired = true, EmitDefaultValue = false)]
        public string VarVersion { get; set; }

        /// <summary>
        /// Gets or Sets Descriptions
        /// </summary>
        [DataMember(Name = "descriptions", IsRequired = true, EmitDefaultValue = false)]
        public List<string> Descriptions { get; set; }

        /// <summary>
        /// Gets or Sets Contributors
        /// </summary>
        [DataMember(Name = "contributors", EmitDefaultValue = false)]
        public List<string>? Contributors { get; set; }

        /// <summary>
        /// Returns true if UpdateInfo instances are equal
        /// </summary>
        /// <param name="input">Instance of UpdateInfo to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(UpdateInfo? input)
        {
            if (input == null)
            {
                return false;
            }

            return
                (
                    VarVersion == input.VarVersion ||
                    VarVersion.Equals(input.VarVersion)
                ) &&
                (
                    Descriptions == input.Descriptions ||
                    Descriptions.SequenceEqual(input.Descriptions)
                ) &&
                (
                    Contributors == input.Contributors ||
                    (Contributors != null &&
                     input.Contributors != null &&
                     Contributors.SequenceEqual(input.Contributors))
                );
        }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class UpdateInfo {\n");
            sb.Append("  VarVersion: ").Append(VarVersion).Append("\n");
            sb.Append("  Descriptions: ").Append(Descriptions).Append("\n");
            sb.Append("  Contributors: ").Append(Contributors).Append("\n");
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
            return Equals(input as UpdateInfo);
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
                hashCode = hashCode * 59 + VarVersion.GetHashCode();
                hashCode = hashCode * 59 + Descriptions.GetHashCode();
                if (Contributors != null)
                {
                    hashCode = hashCode * 59 + Contributors.GetHashCode();
                }

                return hashCode;
            }
        }
    }
}