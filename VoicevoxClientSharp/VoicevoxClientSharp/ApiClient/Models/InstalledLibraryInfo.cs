using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace VoicevoxClientSharp.Models
{
    /// <summary>
    /// インストール済み音声ライブラリの情報
    /// </summary>
    public sealed class InstalledLibraryInfo : IEquatable<InstalledLibraryInfo>
    {
        [JsonConstructor]
        public InstalledLibraryInfo()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InstalledLibraryInfo" /> class.
        /// </summary>
        /// <param name="name">音声ライブラリの名前 (required).</param>
        /// <param name="uuid">音声ライブラリのUUID (required).</param>
        /// <param name="varVersion">音声ライブラリのバージョン (required).</param>
        /// <param name="downloadUrl">音声ライブラリのダウンロードURL (required).</param>
        /// <param name="bytes">音声ライブラリのバイト数 (required).</param>
        /// <param name="speakers">speakers (required).</param>
        /// <param name="uninstallable">アンインストール可能かどうか (required).</param>
        public InstalledLibraryInfo(string name,
            string uuid,
            string varVersion,
            string downloadUrl,
            int bytes,
            LibrarySpeaker[] speakers,
            bool uninstallable)
        {
            Name = name;
            Uuid = uuid;
            VarVersion = varVersion;
            DownloadUrl = downloadUrl;
            Bytes = bytes;
            Speakers = speakers;
            Uninstallable = uninstallable;
        }

        /// <summary>
        /// 音声ライブラリの名前
        /// </summary>
        /// <value>音声ライブラリの名前</value>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// 音声ライブラリのUUID
        /// </summary>
        /// <value>音声ライブラリのUUID</value>
        [DataMember(Name = "uuid", IsRequired = true, EmitDefaultValue = false)]
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


        [JsonPropertyName("speakers")] public LibrarySpeaker[] Speakers { get; set; }

        /// <summary>
        /// アンインストール可能かどうか
        /// </summary>
        /// <value>アンインストール可能かどうか</value>
        [JsonPropertyName("uninstallable")]
        public bool Uninstallable { get; set; }

        /// <summary>
        /// Returns true if InstalledLibraryInfo instances are equal
        /// </summary>
        /// <param name="input">Instance of InstalledLibraryInfo to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(InstalledLibraryInfo? input)
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
                ) &&
                (
                    Uninstallable == input.Uninstallable ||
                    Uninstallable.Equals(input.Uninstallable)
                );
        }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class InstalledLibraryInfo {\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Uuid: ").Append(Uuid).Append("\n");
            sb.Append("  VarVersion: ").Append(VarVersion).Append("\n");
            sb.Append("  DownloadUrl: ").Append(DownloadUrl).Append("\n");
            sb.Append("  Bytes: ").Append(Bytes).Append("\n");
            sb.Append("  Speakers: ").Append(Speakers).Append("\n");
            sb.Append("  Uninstallable: ").Append(Uninstallable).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }


        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return Equals(input as InstalledLibraryInfo);
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
                if (Name != null)
                {
                    hashCode = hashCode * 59 + Name.GetHashCode();
                }

                if (Uuid != null)
                {
                    hashCode = hashCode * 59 + Uuid.GetHashCode();
                }

                if (VarVersion != null)
                {
                    hashCode = hashCode * 59 + VarVersion.GetHashCode();
                }

                if (DownloadUrl != null)
                {
                    hashCode = hashCode * 59 + DownloadUrl.GetHashCode();
                }

                hashCode = hashCode * 59 + Bytes.GetHashCode();
                if (Speakers != null)
                {
                    hashCode = hashCode * 59 + Speakers.GetHashCode();
                }

                hashCode = hashCode * 59 + Uninstallable.GetHashCode();
                return hashCode;
            }
        }
    }
}