using System;
using System.Runtime.Serialization;
using System.Text;

// たぶん未使用
namespace VoicevoxClientSharp.Models
{
    /// <summary>
    /// vvlib(VOICEVOX Library)に関する情報
    /// </summary>
    [DataContract(Name = "VvlibManifest")]
    public sealed class VvlibManifest : IEquatable<VvlibManifest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VvlibManifest" /> class.
        /// </summary>
        /// <param name="manifestVersion">マニフェストバージョン (required).</param>
        /// <param name="name">音声ライブラリ名 (required).</param>
        /// <param name="varVersion">音声ライブラリバージョン (required).</param>
        /// <param name="uuid">音声ライブラリのUUID (required).</param>
        /// <param name="brandName">エンジンのブランド名 (required).</param>
        /// <param name="engineName">エンジン名 (required).</param>
        /// <param name="engineUuid">エンジンのUUID (required).</param>
        public VvlibManifest(string manifestVersion,
            string name,
            string varVersion,
            string uuid,
            string brandName,
            string engineName,
            string engineUuid)
        {
            ManifestVersion = manifestVersion;
            Name = name;
            VarVersion = varVersion;
            Uuid = uuid;
            BrandName = brandName;
            EngineName = engineName;
            EngineUuid = engineUuid;
        }

        /// <summary>
        /// マニフェストバージョン
        /// </summary>
        /// <value>マニフェストバージョン</value>
        [DataMember(Name = "manifest_version", IsRequired = true, EmitDefaultValue = false)]
        public string ManifestVersion { get; set; }

        /// <summary>
        /// 音声ライブラリ名
        /// </summary>
        /// <value>音声ライブラリ名</value>
        [DataMember(Name = "name", IsRequired = true, EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        /// 音声ライブラリバージョン
        /// </summary>
        /// <value>音声ライブラリバージョン</value>
        [DataMember(Name = "version", IsRequired = true, EmitDefaultValue = false)]
        public string VarVersion { get; set; }

        /// <summary>
        /// 音声ライブラリのUUID
        /// </summary>
        /// <value>音声ライブラリのUUID</value>
        [DataMember(Name = "uuid", IsRequired = true, EmitDefaultValue = false)]
        public string Uuid { get; set; }

        /// <summary>
        /// エンジンのブランド名
        /// </summary>
        /// <value>エンジンのブランド名</value>
        [DataMember(Name = "brand_name", IsRequired = true, EmitDefaultValue = false)]
        public string BrandName { get; set; }

        /// <summary>
        /// エンジン名
        /// </summary>
        /// <value>エンジン名</value>
        [DataMember(Name = "engine_name", IsRequired = true, EmitDefaultValue = false)]
        public string EngineName { get; set; }

        /// <summary>
        /// エンジンのUUID
        /// </summary>
        /// <value>エンジンのUUID</value>
        [DataMember(Name = "engine_uuid", IsRequired = true, EmitDefaultValue = false)]
        public string EngineUuid { get; set; }

        public bool Equals(VvlibManifest? other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return ManifestVersion == other.ManifestVersion && Name == other.Name && VarVersion == other.VarVersion &&
                   Uuid == other.Uuid && BrandName == other.BrandName && EngineName == other.EngineName &&
                   EngineUuid == other.EngineUuid;
        }


        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class VvlibManifest {\n");
            sb.Append("  ManifestVersion: ").Append(ManifestVersion).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  VarVersion: ").Append(VarVersion).Append("\n");
            sb.Append("  Uuid: ").Append(Uuid).Append("\n");
            sb.Append("  BrandName: ").Append(BrandName).Append("\n");
            sb.Append("  EngineName: ").Append(EngineName).Append("\n");
            sb.Append("  EngineUuid: ").Append(EngineUuid).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }


        public override bool Equals(object? obj)
        {
            return ReferenceEquals(this, obj) || (obj is VvlibManifest other && Equals(other));
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = ManifestVersion.GetHashCode();
                hashCode = (hashCode * 397) ^ Name.GetHashCode();
                hashCode = (hashCode * 397) ^ VarVersion.GetHashCode();
                hashCode = (hashCode * 397) ^ Uuid.GetHashCode();
                hashCode = (hashCode * 397) ^ BrandName.GetHashCode();
                hashCode = (hashCode * 397) ^ EngineName.GetHashCode();
                hashCode = (hashCode * 397) ^ EngineUuid.GetHashCode();
                return hashCode;
            }
        }
    }
}