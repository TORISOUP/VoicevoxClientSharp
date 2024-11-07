using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace VoicevoxClientSharp.Models
{
    /// <summary>
    /// キャラクター情報
    /// </summary>
    [DataContract(Name = "Speaker")]
    public sealed class Speaker : IEquatable<Speaker>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Speaker" /> class.
        /// </summary>
        /// <param name="name">名前 (required).</param>
        /// <param name="speakerUuid">キャラクターのUUID (required).</param>
        /// <param name="styles">styles (required).</param>
        /// <param name="varVersion">キャラクターのバージョン (required).</param>
        /// <param name="supportedFeatures">supportedFeatures.</param>
        public Speaker(string name,
            string speakerUuid,
            List<SpeakerStyle> styles,
            string varVersion,
            SpeakerSupportedFeatures? supportedFeatures)
        {
            Name = name;
            SpeakerUuid = speakerUuid;
            Styles = styles;
            VarVersion = varVersion;
            SupportedFeatures = supportedFeatures;
        }

        /// <summary>
        /// 名前
        /// </summary>
        /// <value>名前</value>
        [DataMember(Name = "name", IsRequired = true, EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        /// キャラクターのUUID
        /// </summary>
        /// <value>キャラクターのUUID</value>
        [DataMember(Name = "speaker_uuid", IsRequired = true, EmitDefaultValue = false)]
        public string SpeakerUuid { get; set; }

        /// <summary>
        /// Gets or Sets Styles
        /// </summary>
        [DataMember(Name = "styles", IsRequired = true, EmitDefaultValue = false)]
        public List<SpeakerStyle> Styles { get; set; }

        /// <summary>
        /// キャラクターのバージョン
        /// </summary>
        /// <value>キャラクターのバージョン</value>
        [DataMember(Name = "version", IsRequired = true, EmitDefaultValue = false)]
        public string VarVersion { get; set; }

        /// <summary>
        /// Gets or Sets SupportedFeatures
        /// </summary>
        [DataMember(Name = "supported_features", EmitDefaultValue = false)]
        public SpeakerSupportedFeatures? SupportedFeatures { get; set; }

        public bool Equals(Speaker? other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Name == other.Name && SpeakerUuid == other.SpeakerUuid && Styles.Equals(other.Styles) &&
                   VarVersion == other.VarVersion && Equals(SupportedFeatures, other.SupportedFeatures);
        }


        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Speaker {\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  SpeakerUuid: ").Append(SpeakerUuid).Append("\n");
            sb.Append("  Styles: ").Append(Styles).Append("\n");
            sb.Append("  VarVersion: ").Append(VarVersion).Append("\n");
            sb.Append("  SupportedFeatures: ").Append(SupportedFeatures).Append("\n");
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
            return ReferenceEquals(this, obj) || (obj is Speaker other && Equals(other));
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Name.GetHashCode();
                hashCode = (hashCode * 397) ^ SpeakerUuid.GetHashCode();
                hashCode = (hashCode * 397) ^ Styles.GetHashCode();
                hashCode = (hashCode * 397) ^ VarVersion.GetHashCode();
                hashCode = (hashCode * 397) ^ (SupportedFeatures != null ? SupportedFeatures.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}