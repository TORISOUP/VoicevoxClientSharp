using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace VoicevoxClientSharp.Models
{
    /// <summary>
    /// 音声合成用のクエリ
    /// </summary>
    [DataContract(Name = "AudioQuery")]
    public sealed class AudioQuery : IEquatable<AudioQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AudioQuery" /> class.
        /// </summary>
        /// <param name="accentPhrases">accentPhrases (required).</param>
        /// <param name="speedScale">全体の話速 (required).</param>
        /// <param name="pitchScale">全体の音高 (required).</param>
        /// <param name="intonationScale">全体の抑揚 (required).</param>
        /// <param name="volumeScale">全体の音量 (required).</param>
        /// <param name="prePhonemeLength">音声の前の無音時間 (required).</param>
        /// <param name="postPhonemeLength">音声の後の無音時間 (required).</param>
        /// <param name="pauseLength">pauseLength.</param>
        /// <param name="pauseLengthScale">句読点などの無音時間（倍率）。デフォルト値は1 (default to 1M).</param>
        /// <param name="outputSamplingRate">音声データの出力サンプリングレート (required).</param>
        /// <param name="outputStereo">音声データをステレオ出力するか否か (required).</param>
        /// <param name="kana">[読み取り専用]AquesTalk 風記法によるテキスト。音声合成用のクエリとしては無視される.</param>
        public AudioQuery(List<AccentPhrase> accentPhrases,
            decimal speedScale,
            decimal pitchScale,
            decimal intonationScale,
            decimal volumeScale,
            decimal prePhonemeLength,
            decimal postPhonemeLength,
            int outputSamplingRate,
            bool outputStereo,
            string? kana,
            decimal? pauseLength,
            decimal pauseLengthScale = 1M)
        {
            AccentPhrases = accentPhrases;
            SpeedScale = speedScale;
            PitchScale = pitchScale;
            IntonationScale = intonationScale;
            VolumeScale = volumeScale;
            PrePhonemeLength = prePhonemeLength;
            PostPhonemeLength = postPhonemeLength;
            OutputSamplingRate = outputSamplingRate;
            OutputStereo = outputStereo;
            PauseLength = pauseLength;
            PauseLengthScale = pauseLengthScale;
            Kana = kana;
        }

        /// <summary>
        /// Gets or Sets AccentPhrases
        /// </summary>
        [DataMember(Name = "accent_phrases", IsRequired = true, EmitDefaultValue = false)]
        public List<AccentPhrase> AccentPhrases { get; set; }

        /// <summary>
        /// 全体の話速
        /// </summary>
        /// <value>全体の話速</value>
        [DataMember(Name = "speedScale", IsRequired = true, EmitDefaultValue = false)]
        public decimal SpeedScale { get; set; }

        /// <summary>
        /// 全体の音高
        /// </summary>
        /// <value>全体の音高</value>
        [DataMember(Name = "pitchScale", IsRequired = true, EmitDefaultValue = false)]
        public decimal PitchScale { get; set; }

        /// <summary>
        /// 全体の抑揚
        /// </summary>
        /// <value>全体の抑揚</value>
        [DataMember(Name = "intonationScale", IsRequired = true, EmitDefaultValue = false)]
        public decimal IntonationScale { get; set; }

        /// <summary>
        /// 全体の音量
        /// </summary>
        /// <value>全体の音量</value>
        [DataMember(Name = "volumeScale", IsRequired = true, EmitDefaultValue = false)]
        public decimal VolumeScale { get; set; }

        /// <summary>
        /// 音声の前の無音時間
        /// </summary>
        /// <value>音声の前の無音時間</value>
        [DataMember(Name = "prePhonemeLength", IsRequired = true, EmitDefaultValue = false)]
        public decimal PrePhonemeLength { get; set; }

        /// <summary>
        /// 音声の後の無音時間
        /// </summary>
        /// <value>音声の後の無音時間</value>
        [DataMember(Name = "postPhonemeLength", IsRequired = true, EmitDefaultValue = false)]
        public decimal PostPhonemeLength { get; set; }

        /// <summary>
        /// Gets or Sets PauseLength
        /// </summary>
        [DataMember(Name = "pauseLength", EmitDefaultValue = true)]
        public decimal? PauseLength { get; set; }

        /// <summary>
        /// 句読点などの無音時間（倍率）。デフォルト値は1
        /// </summary>
        /// <value>句読点などの無音時間（倍率）。デフォルト値は1</value>
        [DataMember(Name = "pauseLengthScale", EmitDefaultValue = false)]
        public decimal? PauseLengthScale { get; set; }

        /// <summary>
        /// 音声データの出力サンプリングレート
        /// </summary>
        /// <value>音声データの出力サンプリングレート</value>
        [DataMember(Name = "outputSamplingRate", IsRequired = true, EmitDefaultValue = false)]
        public int OutputSamplingRate { get; set; }

        /// <summary>
        /// 音声データをステレオ出力するか否か
        /// </summary>
        /// <value>音声データをステレオ出力するか否か</value>
        [DataMember(Name = "outputStereo", IsRequired = true, EmitDefaultValue = false)]
        public bool OutputStereo { get; set; }

        /// <summary>
        /// [読み取り専用]AquesTalk 風記法によるテキスト。音声合成用のクエリとしては無視される
        /// </summary>
        /// <value>[読み取り専用]AquesTalk 風記法によるテキスト。音声合成用のクエリとしては無視される</value>
        [DataMember(Name = "kana", EmitDefaultValue = true)]
        public string? Kana { get; set; }

        /// <summary>
        /// Returns true if AudioQuery instances are equal
        /// </summary>
        /// <param name="input">Instance of AudioQuery to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(AudioQuery? input)
        {
            if (input == null)
            {
                return false;
            }

            return
                (
                    AccentPhrases == input.AccentPhrases ||
                    AccentPhrases.SequenceEqual(input.AccentPhrases)
                ) &&
                (
                    SpeedScale == input.SpeedScale ||
                    SpeedScale.Equals(input.SpeedScale)
                ) &&
                (
                    PitchScale == input.PitchScale ||
                    PitchScale.Equals(input.PitchScale)
                ) &&
                (
                    IntonationScale == input.IntonationScale ||
                    IntonationScale.Equals(input.IntonationScale)
                ) &&
                (
                    VolumeScale == input.VolumeScale ||
                    VolumeScale.Equals(input.VolumeScale)
                ) &&
                (
                    PrePhonemeLength == input.PrePhonemeLength ||
                    PrePhonemeLength.Equals(input.PrePhonemeLength)
                ) &&
                (
                    PostPhonemeLength == input.PostPhonemeLength ||
                    PostPhonemeLength.Equals(input.PostPhonemeLength)
                ) &&
                (
                    PauseLength == input.PauseLength ||
                    (PauseLength != null &&
                     PauseLength.Equals(input.PauseLength))
                ) &&
                (
                    PauseLengthScale == input.PauseLengthScale ||
                    PauseLengthScale.Equals(input.PauseLengthScale)
                ) &&
                (
                    OutputSamplingRate == input.OutputSamplingRate ||
                    OutputSamplingRate.Equals(input.OutputSamplingRate)
                ) &&
                (
                    OutputStereo == input.OutputStereo ||
                    OutputStereo.Equals(input.OutputStereo)
                ) &&
                (
                    Kana == input.Kana ||
                    (Kana != null &&
                     Kana.Equals(input.Kana))
                );
        }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class AudioQuery {\n");
            sb.Append("  AccentPhrases: ").Append(AccentPhrases).Append("\n");
            sb.Append("  SpeedScale: ").Append(SpeedScale).Append("\n");
            sb.Append("  PitchScale: ").Append(PitchScale).Append("\n");
            sb.Append("  IntonationScale: ").Append(IntonationScale).Append("\n");
            sb.Append("  VolumeScale: ").Append(VolumeScale).Append("\n");
            sb.Append("  PrePhonemeLength: ").Append(PrePhonemeLength).Append("\n");
            sb.Append("  PostPhonemeLength: ").Append(PostPhonemeLength).Append("\n");
            sb.Append("  PauseLength: ").Append(PauseLength).Append("\n");
            sb.Append("  PauseLengthScale: ").Append(PauseLengthScale).Append("\n");
            sb.Append("  OutputSamplingRate: ").Append(OutputSamplingRate).Append("\n");
            sb.Append("  OutputStereo: ").Append(OutputStereo).Append("\n");
            sb.Append("  Kana: ").Append(Kana).Append("\n");
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

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object? input)
        {
            return Equals(input as AudioQuery);
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
                hashCode = hashCode * 59 + AccentPhrases.GetHashCode();

                hashCode = hashCode * 59 + SpeedScale.GetHashCode();
                hashCode = hashCode * 59 + PitchScale.GetHashCode();
                hashCode = hashCode * 59 + IntonationScale.GetHashCode();
                hashCode = hashCode * 59 + VolumeScale.GetHashCode();
                hashCode = hashCode * 59 + PrePhonemeLength.GetHashCode();
                hashCode = hashCode * 59 + PostPhonemeLength.GetHashCode();
                if (PauseLength != null)
                {
                    hashCode = hashCode * 59 + PauseLength.GetHashCode();
                }

                hashCode = hashCode * 59 + PauseLengthScale.GetHashCode();
                hashCode = hashCode * 59 + OutputSamplingRate.GetHashCode();
                hashCode = hashCode * 59 + OutputStereo.GetHashCode();
                if (Kana != null)
                {
                    hashCode = hashCode * 59 + Kana.GetHashCode();
                }

                return hashCode;
            }
        }
    }
}