using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace VoicevoxClientSharp.Models
{
    /// <summary>
    /// ダウンロード可能な音声ライブラリの情報
    /// </summary>
    public sealed class DownloadableLibraryInfo : IEquatable<DownloadableLibraryInfo>
    {
        [JsonConstructor]
        public DownloadableLibraryInfo()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadableLibraryInfo" /> class.
        /// </summary>
        /// <param name="name">音声ライブラリの名前 (required).</param>
        /// <param name="uuid">音声ライブラリのUUID (required).</param>
        /// <param name="varVersion">音声ライブラリのバージョン (required).</param>
        /// <param name="downloadUrl">音声ライブラリのダウンロードURL (required).</param>
        /// <param name="bytes">音声ライブラリのバイト数 (required).</param>
        /// <param name="speakers">speakers (required).</param>
        public DownloadableLibraryInfo(string name,
            string uuid,
            string varVersion,
            string downloadUrl,
            int bytes,
            LibrarySpeaker[] speakers)
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
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// 音声ライブラリのUUID
        /// </summary>
        /// <value>音声ライブラリのUUID</value>
        [JsonPropertyName("uuid")]
        public string Uuid { get; set; }

        /// <summary>
        /// 音声ライブラリのバージョン
        /// </summary>
        /// <value>音声ライブラリのバージョン</value>
        [JsonPropertyName("version")]
        public string VarVersion { get; set; }

        /// <summary>
        /// 音声ライブラリのダウンロードURL
        /// </summary>
        /// <value>音声ライブラリのダウンロードURL</value>
        [JsonPropertyName("download_url")]
        public string DownloadUrl { get; set; }

        /// <summary>
        /// 音声ライブラリのバイト数
        /// </summary>
        /// <value>音声ライブラリのバイト数</value>
        [JsonPropertyName("bytes")]
        public int Bytes { get; set; }

        /// <summary>
        /// Gets or Sets Speakers
        /// </summary>
        [JsonPropertyName("speakers")]
        public LibrarySpeaker[] Speakers { get; set; }

        /// <summary>
        /// Returns true if DownloadableLibraryInfo instances are equal
        /// </summary>
        /// <param name="input">Instance of DownloadableLibraryInfo to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(DownloadableLibraryInfo? input)
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
            sb.Append("class DownloadableLibraryInfo {\n");
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
            return Equals(input as DownloadableLibraryInfo);
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