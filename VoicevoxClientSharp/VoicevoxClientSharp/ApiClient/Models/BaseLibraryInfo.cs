using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

// たぶん未使用
namespace VoicevoxClientSharp.ApiClient.Models
{
    /// <summary>
    /// 音声ライブラリの情報
    /// </summary>
    [DataContract(Name = "BaseLibraryInfo")]
    public sealed class BaseLibraryInfo : IEquatable<BaseLibraryInfo>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseLibraryInfo" /> class.
        /// </summary>
        /// <param name="name">音声ライブラリの名前 (required).</param>
        /// <param name="uuid">音声ライブラリのUUID (required).</param>
        /// <param name="varVersion">音声ライブラリのバージョン (required).</param>
        /// <param name="downloadUrl">音声ライブラリのダウンロードURL (required).</param>
        /// <param name="bytes">音声ライブラリのバイト数 (required).</param>
        /// <param name="speakers">speakers (required).</param>
        public BaseLibraryInfo(string name,
            string uuid,
            string varVersion,
            string downloadUrl,
            int bytes,
            List<LibrarySpeaker> speakers)
        {
            Name = name;
            Uuid = uuid;
            VarVersion = varVersion;
            DownloadUrl = downloadUrl;
            Bytes = bytes;
            Speakers = speakers;
        }

        /// <summary>
        /// 音声ライブラリの名前
        /// </summary>
        /// <value>音声ライブラリの名前</value>
        [DataMember(Name = "name", IsRequired = true, EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        /// 音声ライブラリのUUID
        /// </summary>
        /// <value>音声ライブラリのUUID</value>
        [DataMember(Name = "uuid", IsRequired = true, EmitDefaultValue = false)]
        public string Uuid { get; set; }

        /// <summary>
        /// 音声ライブラリのバージョン
        /// </summary>
        /// <value>音声ライブラリのバージョン</value>
        [DataMember(Name = "version", IsRequired = true, EmitDefaultValue = false)]
        public string VarVersion { get; set; }

        /// <summary>
        /// 音声ライブラリのダウンロードURL
        /// </summary>
        /// <value>音声ライブラリのダウンロードURL</value>
        [DataMember(Name = "download_url", IsRequired = true, EmitDefaultValue = false)]
        public string DownloadUrl { get; set; }

        /// <summary>
        /// 音声ライブラリのバイト数
        /// </summary>
        /// <value>音声ライブラリのバイト数</value>
        [DataMember(Name = "bytes", IsRequired = true, EmitDefaultValue = false)]
        public int Bytes { get; set; }

        /// <summary>
        /// Gets or Sets Speakers
        /// </summary>
        [DataMember(Name = "speakers", IsRequired = true, EmitDefaultValue = false)]
        public List<LibrarySpeaker> Speakers { get; set; }

        /// <summary>
        /// Returns true if BaseLibraryInfo instances are equal
        /// </summary>
        /// <param name="input">Instance of BaseLibraryInfo to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(BaseLibraryInfo? input)
        {
            if (input == null)
            {
                return false;
            }

            return
                (
                    Name == input.Name ||
                    Name.Equals(input.Name)
                ) &&
                (
                    Uuid == input.Uuid ||
                    Uuid.Equals(input.Uuid)
                ) &&
                (
                    VarVersion == input.VarVersion ||
                    VarVersion.Equals(input.VarVersion)
                ) &&
                (
                    DownloadUrl == input.DownloadUrl ||
                    DownloadUrl.Equals(input.DownloadUrl)
                ) &&
                (
                    Bytes == input.Bytes ||
                    Bytes.Equals(input.Bytes)
                ) &&
                (
                    Speakers == input.Speakers ||
                    Speakers.SequenceEqual(input.Speakers)
                );
        }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class BaseLibraryInfo {\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Uuid: ").Append(Uuid).Append("\n");
            sb.Append("  VarVersion: ").Append(VarVersion).Append("\n");
            sb.Append("  DownloadUrl: ").Append(DownloadUrl).Append("\n");
            sb.Append("  Bytes: ").Append(Bytes).Append("\n");
            sb.Append("  Speakers: ").Append(Speakers).Append("\n");
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
            return Equals(input as BaseLibraryInfo);
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
                hashCode = hashCode * 59 + Name.GetHashCode();
                hashCode = hashCode * 59 + Uuid.GetHashCode();
                hashCode = hashCode * 59 + VarVersion.GetHashCode();
                hashCode = hashCode * 59 + DownloadUrl.GetHashCode();
                hashCode = hashCode * 59 + Bytes.GetHashCode();
                hashCode = hashCode * 59 + Speakers.GetHashCode();

                return hashCode;
            }
        }
    }
}