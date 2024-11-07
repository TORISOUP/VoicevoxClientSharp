using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace VoicevoxClientSharp.Models
{
    /// <summary>
    /// キャラクターの追加情報
    /// </summary>
    [DataContract(Name = "SpeakerInfo")]
    public sealed class SpeakerInfo : IEquatable<SpeakerInfo>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpeakerInfo" /> class.
        /// </summary>
        /// <param name="policy">policy.md (required).</param>
        /// <param name="portrait">立ち絵画像をbase64エンコードしたもの、あるいはURL (required).</param>
        /// <param name="styleInfos">styleInfos (required).</param>
        public SpeakerInfo(string policy, string portrait, List<StyleInfo> styleInfos)
        {
            Policy = policy;
            Portrait = portrait;
            StyleInfos = styleInfos;
        }

        /// <summary>
        /// policy.md
        /// </summary>
        /// <value>policy.md</value>
        [DataMember(Name = "policy", IsRequired = true, EmitDefaultValue = false)]
        public string Policy { get; set; }

        /// <summary>
        /// 立ち絵画像をbase64エンコードしたもの、あるいはURL
        /// </summary>
        /// <value>立ち絵画像をbase64エンコードしたもの、あるいはURL</value>
        [DataMember(Name = "portrait", IsRequired = true, EmitDefaultValue = false)]
        public string Portrait { get; set; }

        /// <summary>
        /// Gets or Sets StyleInfos
        /// </summary>
        [DataMember(Name = "style_infos", IsRequired = true, EmitDefaultValue = false)]
        public List<StyleInfo> StyleInfos { get; set; }


        public bool Equals(SpeakerInfo? other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Policy == other.Policy && Portrait == other.Portrait && StyleInfos.Equals(other.StyleInfos);
        }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class SpeakerInfo {\n");
            sb.Append("  Policy: ").Append(Policy).Append("\n");
            sb.Append("  Portrait: ").Append(Portrait).Append("\n");
            sb.Append("  StyleInfos: ").Append(StyleInfos).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        public override bool Equals(object? obj)
        {
            return ReferenceEquals(this, obj) || (obj is SpeakerInfo other && Equals(other));
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Policy.GetHashCode();
                hashCode = (hashCode * 397) ^ Portrait.GetHashCode();
                hashCode = (hashCode * 397) ^ StyleInfos.GetHashCode();
                return hashCode;
            }
        }
    }
}