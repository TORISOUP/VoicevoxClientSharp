using System;
using System.Text;
using System.Text.Json.Serialization;

namespace VoicevoxClientSharp.ApiClient.Models
{
    /// <summary>
    /// キャラクターの追加情報
    /// </summary>
    public sealed class SpeakerInfo : IEquatable<SpeakerInfo>
    {
        [JsonConstructor]
        public SpeakerInfo()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SpeakerInfo" /> class.
        /// </summary>
        /// <param name="policy">policy.md (required).</param>
        /// <param name="portrait">立ち絵画像をbase64エンコードしたもの、あるいはURL (required).</param>
        /// <param name="styleInfos">styleInfos (required).</param>
        public SpeakerInfo(string policy, string portrait, StyleInfo[] styleInfos)
        {
            Policy = policy;
            Portrait = portrait;
            StyleInfos = styleInfos;
        }

        /// <summary>
        /// policy.md
        /// </summary>
        /// <value>policy.md</value>
        [JsonPropertyName("policy")]
        public string Policy { get; set; }

        /// <summary>
        /// 立ち絵画像をbase64エンコードしたもの、あるいはURL
        /// </summary>
        /// <value>立ち絵画像をbase64エンコードしたもの、あるいはURL</value>
        [JsonPropertyName("portrait")]
        public string Portrait { get; set; }

        /// <summary>
        /// Gets or Sets StyleInfos
        /// </summary>
        [JsonPropertyName("styles")]
        public StyleInfo[] StyleInfos { get; set; } = Array.Empty<StyleInfo>();


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