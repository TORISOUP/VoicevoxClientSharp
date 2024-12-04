using System;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace VoicevoxClientSharp.ApiClient.Models
{
    /// <summary>
    /// 依存ライブラリのライセンス情報
    /// </summary>
    public sealed class LicenseInfo : IEquatable<LicenseInfo>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LicenseInfo" /> class.
        /// </summary>
        /// <param name="name">依存ライブラリ名 (required).</param>
        /// <param name="varVersion">依存ライブラリのバージョン.</param>
        /// <param name="license">依存ライブラリのライセンス名.</param>
        /// <param name="text">依存ライブラリのライセンス本文 (required).</param>
        public LicenseInfo(string name,
            string? varVersion,
            string? license,
            string text)
        {
            Name = name;
            Text = text;
            VarVersion = varVersion;
            License = license;
        }

        /// <summary>
        /// 依存ライブラリ名
        /// </summary>
        /// <value>依存ライブラリ名</value>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// 依存ライブラリのバージョン
        /// </summary>
        /// <value>依存ライブラリのバージョン</value>
        [DataMember(Name = "version", EmitDefaultValue = false)]
        [JsonPropertyName("version")]
        public string? VarVersion { get; set; }

        /// <summary>
        /// 依存ライブラリのライセンス名
        /// </summary>
        /// <value>依存ライブラリのライセンス名</value>
        [DataMember(Name = "license", EmitDefaultValue = false)]
        [JsonPropertyName("license")]
        public string? License { get; set; }

        /// <summary>
        /// 依存ライブラリのライセンス本文
        /// </summary>
        /// <value>依存ライブラリのライセンス本文</value>
        [DataMember(Name = "text", IsRequired = true, EmitDefaultValue = false)]
        [JsonPropertyName("text")]
        public string Text { get; set; }


        public bool Equals(LicenseInfo? other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Name == other.Name && VarVersion == other.VarVersion && License == other.License &&
                   Text == other.Text;
        }


        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class LicenseInfo {\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  VarVersion: ").Append(VarVersion).Append("\n");
            sb.Append("  License: ").Append(License).Append("\n");
            sb.Append("  Text: ").Append(Text).Append("\n");
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
            return ReferenceEquals(this, input) || (input is LicenseInfo other && Equals(other));
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Name.GetHashCode();
                hashCode = (hashCode * 397) ^ (VarVersion != null ? VarVersion.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (License != null ? License.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Text.GetHashCode();
                return hashCode;
            }
        }
    }
}