using System;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace VoicevoxClientSharp.ApiClient.Models
{
    /// <summary>
    /// エンジン自体に関する情報
    /// </summary>
    public sealed class EngineManifest : IEquatable<EngineManifest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EngineManifest" /> class.
        /// </summary>
        /// <param name="manifestVersion">マニフェストのバージョン (required).</param>
        /// <param name="name">エンジン名 (required).</param>
        /// <param name="brandName">ブランド名 (required).</param>
        /// <param name="uuid">エンジンのUUID (required).</param>
        /// <param name="url">エンジンのURL (required).</param>
        /// <param name="icon">エンジンのアイコンをBASE64エンコードしたもの (required).</param>
        /// <param name="defaultSamplingRate">デフォルトのサンプリング周波数 (required).</param>
        /// <param name="frameRate">エンジンのフレームレート (required).</param>
        /// <param name="termsOfService">エンジンの利用規約 (required).</param>
        /// <param name="updateInfos">updateInfos (required).</param>
        /// <param name="dependencyLicenses">dependencyLicenses (required).</param>
        /// <param name="supportedVvlibManifestVersion">エンジンが対応するvvlibのバージョン.</param>
        /// <param name="supportedFeatures">supportedFeatures (required).</param>
        public EngineManifest(string manifestVersion,
            string name,
            string brandName,
            string uuid,
            string url,
            string icon,
            int defaultSamplingRate,
            decimal frameRate,
            string termsOfService,
            UpdateInfo[] updateInfos,
            LicenseInfo[] dependencyLicenses,
            string? supportedVvlibManifestVersion,
            SupportedFeatures supportedFeatures)
        {
            ManifestVersion = manifestVersion;
            Name = name;
            BrandName = brandName;
            Uuid = uuid;
            Url = url;
            Icon = icon;
            FrameRate = frameRate;
            TermsOfService = termsOfService;
            DefaultSamplingRate = defaultSamplingRate;
            UpdateInfos = updateInfos;
            DependencyLicenses = dependencyLicenses;
            SupportedFeatures = supportedFeatures;
            SupportedVvlibManifestVersion = supportedVvlibManifestVersion;
        }

        /// <summary>
        /// マニフェストのバージョン
        /// </summary>
        /// <value>マニフェストのバージョン</value>
        [JsonPropertyName("manifest_version")]
        public string ManifestVersion { get; set; }

        /// <summary>
        /// エンジン名
        /// </summary>
        /// <value>エンジン名</value>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// ブランド名
        /// </summary>
        /// <value>ブランド名</value>
        [JsonPropertyName("brand_name")]
        public string BrandName { get; set; }

        /// <summary>
        /// エンジンのUUID
        /// </summary>
        /// <value>エンジンのUUID</value>
        [JsonPropertyName("uuid")]
        public string Uuid { get; set; }

        /// <summary>
        /// エンジンのURL
        /// </summary>
        /// <value>エンジンのURL</value>
        [JsonPropertyName("url")]
        public string Url { get; set; }

        /// <summary>
        /// エンジンのアイコンをBASE64エンコードしたもの
        /// </summary>
        /// <value>エンジンのアイコンをBASE64エンコードしたもの</value>
        [JsonPropertyName("icon")]
        public string Icon { get; set; }

        /// <summary>
        /// デフォルトのサンプリング周波数
        /// </summary>
        /// <value>デフォルトのサンプリング周波数</value>
        [JsonPropertyName("default_sampling_rate")]
        public int DefaultSamplingRate { get; set; }

        /// <summary>
        /// エンジンのフレームレート
        /// </summary>
        /// <value>エンジンのフレームレート</value>
        [JsonPropertyName("frame_rate")]
        public decimal FrameRate { get; set; }

        /// <summary>
        /// エンジンの利用規約
        /// </summary>
        /// <value>エンジンの利用規約</value>
        [JsonPropertyName("terms_of_service")]
        public string TermsOfService { get; set; }


        [JsonPropertyName("update_infos")] public UpdateInfo[] UpdateInfos { get; set; }


        [JsonPropertyName("dependency_licenses")]
        public LicenseInfo[] DependencyLicenses { get; set; }

        /// <summary>
        /// エンジンが対応するvvlibのバージョン
        /// </summary>
        /// <value>エンジンが対応するvvlibのバージョン</value>
        [JsonPropertyName("supported_vvlib_manifest_version")]
        public string? SupportedVvlibManifestVersion { get; set; }


        [JsonPropertyName("supported_features")]
        public SupportedFeatures SupportedFeatures { get; set; }

        /// <summary>
        /// Returns true if EngineManifest instances are equal
        /// </summary>
        /// <param name="input">Instance of EngineManifest to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(EngineManifest? input)
        {
            if (input == null)
            {
                return false;
            }

            return
                (
                    ManifestVersion == input.ManifestVersion ||
                    ManifestVersion.Equals(input.ManifestVersion)
                ) &&
                (
                    Name == input.Name ||
                    Name.Equals(input.Name)
                ) &&
                (
                    BrandName == input.BrandName ||
                    BrandName.Equals(input.BrandName)
                ) &&
                (
                    Uuid == input.Uuid ||
                    Uuid.Equals(input.Uuid)
                ) &&
                (
                    Url == input.Url ||
                    Url.Equals(input.Url)
                ) &&
                (
                    Icon == input.Icon ||
                    Icon.Equals(input.Icon)
                ) &&
                (
                    DefaultSamplingRate == input.DefaultSamplingRate ||
                    DefaultSamplingRate.Equals(input.DefaultSamplingRate)
                ) &&
                (
                    FrameRate == input.FrameRate ||
                    FrameRate.Equals(input.FrameRate)
                ) &&
                (
                    TermsOfService == input.TermsOfService ||
                    TermsOfService.Equals(input.TermsOfService)
                ) &&
                (
                    UpdateInfos == input.UpdateInfos ||
                    UpdateInfos.SequenceEqual(input.UpdateInfos)
                ) &&
                (
                    DependencyLicenses == input.DependencyLicenses ||
                    DependencyLicenses.SequenceEqual(input.DependencyLicenses)
                ) &&
                (
                    SupportedVvlibManifestVersion == input.SupportedVvlibManifestVersion ||
                    (SupportedVvlibManifestVersion != null &&
                     SupportedVvlibManifestVersion.Equals(input.SupportedVvlibManifestVersion))
                ) &&
                (
                    SupportedFeatures.Equals(input.SupportedFeatures) ||
                    SupportedFeatures.Equals(input.SupportedFeatures)
                );
        }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class EngineManifest {\n");
            sb.Append("  ManifestVersion: ").Append(ManifestVersion).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  BrandName: ").Append(BrandName).Append("\n");
            sb.Append("  Uuid: ").Append(Uuid).Append("\n");
            sb.Append("  Url: ").Append(Url).Append("\n");
            sb.Append("  Icon: ").Append(Icon).Append("\n");
            sb.Append("  DefaultSamplingRate: ").Append(DefaultSamplingRate).Append("\n");
            sb.Append("  FrameRate: ").Append(FrameRate).Append("\n");
            sb.Append("  TermsOfService: ").Append(TermsOfService).Append("\n");
            sb.Append("  UpdateInfos: ").Append(UpdateInfos).Append("\n");
            sb.Append("  DependencyLicenses: ").Append(DependencyLicenses).Append("\n");
            sb.Append("  SupportedVvlibManifestVersion: ").Append(SupportedVvlibManifestVersion).Append("\n");
            sb.Append("  SupportedFeatures: ").Append(SupportedFeatures).Append("\n");
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
            return Equals(input as EngineManifest);
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
                hashCode = hashCode * 59 + ManifestVersion.GetHashCode();
                hashCode = hashCode * 59 + Name.GetHashCode();
                hashCode = hashCode * 59 + BrandName.GetHashCode();
                hashCode = hashCode * 59 + Uuid.GetHashCode();
                hashCode = hashCode * 59 + Url.GetHashCode();
                hashCode = hashCode * 59 + Icon.GetHashCode();
                hashCode = hashCode * 59 + DefaultSamplingRate.GetHashCode();
                hashCode = hashCode * 59 + FrameRate.GetHashCode();
                hashCode = hashCode * 59 + TermsOfService.GetHashCode();
                hashCode = hashCode * 59 + UpdateInfos.GetHashCode();
                hashCode = hashCode * 59 + DependencyLicenses.GetHashCode();
                if (SupportedVvlibManifestVersion != null)
                {
                    hashCode = hashCode * 59 + SupportedVvlibManifestVersion.GetHashCode();
                }

                hashCode = hashCode * 59 + SupportedFeatures.GetHashCode();

                return hashCode;
            }
        }
    }
}