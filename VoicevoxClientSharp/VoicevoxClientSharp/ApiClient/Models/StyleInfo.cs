using System;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace VoicevoxClientSharp.ApiClient.Models
{
    /// <summary>
    /// スタイルの追加情報
    /// </summary>
    public sealed class StyleInfo : IEquatable<StyleInfo>
    {
        [JsonConstructor]
        public StyleInfo()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StyleInfo" /> class.
        /// </summary>
        /// <param name="id">スタイルID (required).</param>
        /// <param name="icon">このスタイルのアイコンをbase64エンコードしたもの、あるいはURL (required).</param>
        /// <param name="portrait">このスタイルの立ち絵画像をbase64エンコードしたもの、あるいはURL.</param>
        /// <param name="voiceSamples">voiceSamples (required).</param>
        public StyleInfo(int id,
            string icon,
            string? portrait,
            string[] voiceSamples)
        {
            Id = id;
            Icon = icon;
            VoiceSamples = voiceSamples;
            Portrait = portrait;
        }

        /// <summary>
        /// スタイルID
        /// </summary>
        /// <value>スタイルID</value>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// このスタイルのアイコンをbase64エンコードしたもの、あるいはURL
        /// </summary>
        /// <value>このスタイルのアイコンをbase64エンコードしたもの、あるいはURL</value>
        [DataMember(Name = "icon", IsRequired = true, EmitDefaultValue = false)]
        [JsonPropertyName("icon")]
        public string Icon { get; set; }

        /// <summary>
        /// このスタイルの立ち絵画像をbase64エンコードしたもの、あるいはURL
        /// </summary>
        /// <value>このスタイルの立ち絵画像をbase64エンコードしたもの、あるいはURL</value>
        [JsonPropertyName("portrait")]
        public string? Portrait { get; set; }


        [JsonPropertyName("voice_samples")] public string[] VoiceSamples { get; set; } = Array.Empty<string>();

        public bool Equals(StyleInfo? other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Id == other.Id && Icon == other.Icon && Portrait == other.Portrait &&
                   VoiceSamples.Equals(other.VoiceSamples);
        }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class StyleInfo {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Icon: ").Append(Icon).Append("\n");
            sb.Append("  Portrait: ").Append(Portrait).Append("\n");
            sb.Append("  VoiceSamples: ").Append(VoiceSamples).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }


        public override bool Equals(object? obj)
        {
            return ReferenceEquals(this, obj) || (obj is StyleInfo other && Equals(other));
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id;
                hashCode = (hashCode * 397) ^ Icon.GetHashCode();
                hashCode = (hashCode * 397) ^ (Portrait != null ? Portrait.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ VoiceSamples.GetHashCode();
                return hashCode;
            }
        }
    }
}