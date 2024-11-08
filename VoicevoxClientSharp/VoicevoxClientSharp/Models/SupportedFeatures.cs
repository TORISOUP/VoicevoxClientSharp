using System;
using System.Runtime.Serialization;
using System.Text;


namespace VoicevoxClientSharp.Models
{
    /// <summary>
    /// エンジンが持つ機能の一覧
    /// </summary>
    [DataContract(Name = "SupportedFeatures")]
    public sealed class SupportedFeatures : IEquatable<SupportedFeatures>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SupportedFeatures" /> class.
        /// </summary>
        /// <param name="adjustMoraPitch">モーラごとの音高の調整 (required).</param>
        /// <param name="adjustPhonemeLength">音素ごとの長さの調整 (required).</param>
        /// <param name="adjustSpeedScale">全体の話速の調整 (required).</param>
        /// <param name="adjustPitchScale">全体の音高の調整 (required).</param>
        /// <param name="adjustIntonationScale">全体の抑揚の調整 (required).</param>
        /// <param name="adjustVolumeScale">全体の音量の調整 (required).</param>
        /// <param name="adjustPauseLength">句読点などの無音時間の調整.</param>
        /// <param name="interrogativeUpspeak">疑問文の自動調整 (required).</param>
        /// <param name="synthesisMorphing">2種類のスタイルでモーフィングした音声を合成 (required).</param>
        /// <param name="sing">歌唱音声合成.</param>
        /// <param name="manageLibrary">音声ライブラリのインストール・アンインストール.</param>
        /// <param name="returnResourceUrl">キャラクター情報のリソースをURLで返送.</param>
        public SupportedFeatures(bool adjustMoraPitch,
            bool adjustPhonemeLength,
            bool adjustSpeedScale,
            bool adjustPitchScale,
            bool adjustIntonationScale,
            bool adjustVolumeScale,
            bool adjustPauseLength,
            bool interrogativeUpspeak,
            bool synthesisMorphing,
            bool? sing,
            bool? manageLibrary,
            bool? returnResourceUrl)
        {
            AdjustMoraPitch = adjustMoraPitch;
            AdjustPhonemeLength = adjustPhonemeLength;
            AdjustSpeedScale = adjustSpeedScale;
            AdjustPitchScale = adjustPitchScale;
            AdjustIntonationScale = adjustIntonationScale;
            AdjustVolumeScale = adjustVolumeScale;
            InterrogativeUpspeak = interrogativeUpspeak;
            SynthesisMorphing = synthesisMorphing;
            AdjustPauseLength = adjustPauseLength;
            Sing = sing;
            ManageLibrary = manageLibrary;
            ReturnResourceUrl = returnResourceUrl;
        }

        /// <summary>
        /// モーラごとの音高の調整
        /// </summary>
        /// <value>モーラごとの音高の調整</value>
        [DataMember(Name = "adjust_mora_pitch", IsRequired = true, EmitDefaultValue = false)]
        public bool AdjustMoraPitch { get; set; }

        /// <summary>
        /// 音素ごとの長さの調整
        /// </summary>
        /// <value>音素ごとの長さの調整</value>
        [DataMember(Name = "adjust_phoneme_length", IsRequired = true, EmitDefaultValue = false)]
        public bool AdjustPhonemeLength { get; set; }

        /// <summary>
        /// 全体の話速の調整
        /// </summary>
        /// <value>全体の話速の調整</value>
        [DataMember(Name = "adjust_speed_scale", IsRequired = true, EmitDefaultValue = false)]
        public bool AdjustSpeedScale { get; set; }

        /// <summary>
        /// 全体の音高の調整
        /// </summary>
        /// <value>全体の音高の調整</value>
        [DataMember(Name = "adjust_pitch_scale", IsRequired = true, EmitDefaultValue = false)]
        public bool AdjustPitchScale { get; set; }

        /// <summary>
        /// 全体の抑揚の調整
        /// </summary>
        /// <value>全体の抑揚の調整</value>
        [DataMember(Name = "adjust_intonation_scale", IsRequired = true, EmitDefaultValue = false)]
        public bool AdjustIntonationScale { get; set; }

        /// <summary>
        /// 全体の音量の調整
        /// </summary>
        /// <value>全体の音量の調整</value>
        [DataMember(Name = "adjust_volume_scale", IsRequired = true, EmitDefaultValue = false)]
        public bool AdjustVolumeScale { get; set; }

        /// <summary>
        /// 句読点などの無音時間の調整
        /// </summary>
        /// <value>句読点などの無音時間の調整</value>
        [DataMember(Name = "adjust_pause_length", EmitDefaultValue = true)]
        public bool AdjustPauseLength { get; set; }

        /// <summary>
        /// 疑問文の自動調整
        /// </summary>
        /// <value>疑問文の自動調整</value>
        [DataMember(Name = "interrogative_upspeak", IsRequired = true, EmitDefaultValue = false)]
        public bool InterrogativeUpspeak { get; set; }

        /// <summary>
        /// 2種類のスタイルでモーフィングした音声を合成
        /// </summary>
        /// <value>2種類のスタイルでモーフィングした音声を合成</value>
        [DataMember(Name = "synthesis_morphing", IsRequired = true, EmitDefaultValue = false)]
        public bool SynthesisMorphing { get; set; }

        /// <summary>
        /// 歌唱音声合成
        /// </summary>
        /// <value>歌唱音声合成</value>
        [DataMember(Name = "sing", EmitDefaultValue = true)]
        public bool? Sing { get; set; }

        /// <summary>
        /// 音声ライブラリのインストール・アンインストール
        /// </summary>
        /// <value>音声ライブラリのインストール・アンインストール</value>
        [DataMember(Name = "manage_library", EmitDefaultValue = true)]
        public bool? ManageLibrary { get; set; }

        /// <summary>
        /// キャラクター情報のリソースをURLで返送
        /// </summary>
        /// <value>キャラクター情報のリソースをURLで返送</value>
        [DataMember(Name = "return_resource_url", EmitDefaultValue = true)]
        public bool? ReturnResourceUrl { get; set; }

        /// <summary>
        /// Returns true if SupportedFeatures instances are equal
        /// </summary>
        /// <param name="input">Instance of SupportedFeatures to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(SupportedFeatures? input)
        {
            if (input == null)
            {
                return false;
            }

            return
                (
                    AdjustMoraPitch == input.AdjustMoraPitch ||
                    AdjustMoraPitch.Equals(input.AdjustMoraPitch)
                ) &&
                (
                    AdjustPhonemeLength == input.AdjustPhonemeLength ||
                    AdjustPhonemeLength.Equals(input.AdjustPhonemeLength)
                ) &&
                (
                    AdjustSpeedScale == input.AdjustSpeedScale ||
                    AdjustSpeedScale.Equals(input.AdjustSpeedScale)
                ) &&
                (
                    AdjustPitchScale == input.AdjustPitchScale ||
                    AdjustPitchScale.Equals(input.AdjustPitchScale)
                ) &&
                (
                    AdjustIntonationScale == input.AdjustIntonationScale ||
                    AdjustIntonationScale.Equals(input.AdjustIntonationScale)
                ) &&
                (
                    AdjustVolumeScale == input.AdjustVolumeScale ||
                    AdjustVolumeScale.Equals(input.AdjustVolumeScale)
                ) &&
                (
                    AdjustPauseLength == input.AdjustPauseLength ||
                    AdjustPauseLength.Equals(input.AdjustPauseLength)
                ) &&
                (
                    InterrogativeUpspeak == input.InterrogativeUpspeak ||
                    InterrogativeUpspeak.Equals(input.InterrogativeUpspeak)
                ) &&
                (
                    SynthesisMorphing == input.SynthesisMorphing ||
                    SynthesisMorphing.Equals(input.SynthesisMorphing)
                ) &&
                (
                    Sing == input.Sing ||
                    Sing.Equals(input.Sing)
                ) &&
                (
                    ManageLibrary == input.ManageLibrary ||
                    ManageLibrary.Equals(input.ManageLibrary)
                ) &&
                (
                    ReturnResourceUrl == input.ReturnResourceUrl ||
                    ReturnResourceUrl.Equals(input.ReturnResourceUrl)
                );
        }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class SupportedFeatures {\n");
            sb.Append("  AdjustMoraPitch: ").Append(AdjustMoraPitch).Append("\n");
            sb.Append("  AdjustPhonemeLength: ").Append(AdjustPhonemeLength).Append("\n");
            sb.Append("  AdjustSpeedScale: ").Append(AdjustSpeedScale).Append("\n");
            sb.Append("  AdjustPitchScale: ").Append(AdjustPitchScale).Append("\n");
            sb.Append("  AdjustIntonationScale: ").Append(AdjustIntonationScale).Append("\n");
            sb.Append("  AdjustVolumeScale: ").Append(AdjustVolumeScale).Append("\n");
            sb.Append("  AdjustPauseLength: ").Append(AdjustPauseLength).Append("\n");
            sb.Append("  InterrogativeUpspeak: ").Append(InterrogativeUpspeak).Append("\n");
            sb.Append("  SynthesisMorphing: ").Append(SynthesisMorphing).Append("\n");
            sb.Append("  Sing: ").Append(Sing).Append("\n");
            sb.Append("  ManageLibrary: ").Append(ManageLibrary).Append("\n");
            sb.Append("  ReturnResourceUrl: ").Append(ReturnResourceUrl).Append("\n");
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
            return Equals(input as SupportedFeatures);
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
                hashCode = hashCode * 59 + AdjustMoraPitch.GetHashCode();
                hashCode = hashCode * 59 + AdjustPhonemeLength.GetHashCode();
                hashCode = hashCode * 59 + AdjustSpeedScale.GetHashCode();
                hashCode = hashCode * 59 + AdjustPitchScale.GetHashCode();
                hashCode = hashCode * 59 + AdjustIntonationScale.GetHashCode();
                hashCode = hashCode * 59 + AdjustVolumeScale.GetHashCode();
                hashCode = hashCode * 59 + AdjustPauseLength.GetHashCode();
                hashCode = hashCode * 59 + InterrogativeUpspeak.GetHashCode();
                hashCode = hashCode * 59 + SynthesisMorphing.GetHashCode();
                hashCode = hashCode * 59 + Sing.GetHashCode();
                hashCode = hashCode * 59 + ManageLibrary.GetHashCode();
                hashCode = hashCode * 59 + ReturnResourceUrl.GetHashCode();
                return hashCode;
            }
        }
    }
}