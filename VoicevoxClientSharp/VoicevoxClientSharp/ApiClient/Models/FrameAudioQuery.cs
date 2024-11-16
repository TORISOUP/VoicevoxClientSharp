using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace VoicevoxClientSharp.ApiClient.Models
{
    /// <summary>
    /// フレームごとの音声合成用のクエリ
    /// </summary>
    [DataContract(Name = "FrameAudioQuery")]
    public sealed class FrameAudioQuery : IEquatable<FrameAudioQuery>
    {
        [JsonConstructor]
        public FrameAudioQuery()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FrameAudioQuery" /> class.
        /// </summary>
        /// <param name="f0">f0 (required).</param>
        /// <param name="volume">volume (required).</param>
        /// <param name="phonemes">phonemes (required).</param>
        /// <param name="volumeScale">全体の音量 (required).</param>
        /// <param name="outputSamplingRate">音声データの出力サンプリングレート (required).</param>
        /// <param name="outputStereo">音声データをステレオ出力するか否か (required).</param>
        public FrameAudioQuery(
            decimal[] f0,
            decimal[] volume,
            FramePhoneme[] phonemes,
            decimal volumeScale,
            int outputSamplingRate,
            bool outputStereo)
        {
            F0 = f0;
            Volume = volume;
            Phonemes = phonemes;
            VolumeScale = volumeScale;
            OutputSamplingRate = outputSamplingRate;
            OutputStereo = outputStereo;
        }


        [JsonPropertyName("f0")] public decimal[] F0 { get; set; } = Array.Empty<decimal>();

        /// <summary>
        /// Gets or Sets Volume
        /// </summary>
        [JsonPropertyName("volume")]
        public decimal[] Volume { get; set; } = Array.Empty<decimal>();

        /// <summary>
        /// Gets or Sets Phonemes
        /// </summary>
        [JsonPropertyName("phonemes")]
        public FramePhoneme[] Phonemes { get; set; } = Array.Empty<FramePhoneme>();

        /// <summary>
        /// 全体の音量
        /// </summary>
        /// <value>全体の音量</value>
        [JsonPropertyName("volumeScale")]
        public decimal VolumeScale { get; set; }

        /// <summary>
        /// 音声データの出力サンプリングレート
        /// </summary>
        /// <value>音声データの出力サンプリングレート</value>
        [JsonPropertyName("outputSamplingRate")]
        public int OutputSamplingRate { get; set; }

        /// <summary>
        /// 音声データをステレオ出力するか否か
        /// </summary>
        /// <value>音声データをステレオ出力するか否か</value>
        [JsonPropertyName("outputStereo")]
        public bool OutputStereo { get; set; }

        /// <summary>
        /// Returns true if FrameAudioQuery instances are equal
        /// </summary>
        /// <param name="input">Instance of FrameAudioQuery to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(FrameAudioQuery? input)
        {
            if (input == null)
            {
                return false;
            }

            return
                (
                    F0 == input.F0 ||
                    F0.SequenceEqual(input.F0)
                ) &&
                (
                    Volume == input.Volume ||
                    Volume.SequenceEqual(input.Volume)
                ) &&
                (
                    Phonemes == input.Phonemes ||
                    Phonemes.SequenceEqual(input.Phonemes)
                ) &&
                (
                    VolumeScale == input.VolumeScale ||
                    VolumeScale.Equals(input.VolumeScale)
                ) &&
                (
                    OutputSamplingRate == input.OutputSamplingRate ||
                    OutputSamplingRate.Equals(input.OutputSamplingRate)
                ) &&
                (
                    OutputStereo == input.OutputStereo ||
                    OutputStereo.Equals(input.OutputStereo)
                );
        }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class FrameAudioQuery {\n");
            sb.Append("  F0: ").Append(F0).Append("\n");
            sb.Append("  Volume: ").Append(Volume).Append("\n");
            sb.Append("  Phonemes: ").Append(Phonemes).Append("\n");
            sb.Append("  VolumeScale: ").Append(VolumeScale).Append("\n");
            sb.Append("  OutputSamplingRate: ").Append(OutputSamplingRate).Append("\n");
            sb.Append("  OutputStereo: ").Append(OutputStereo).Append("\n");
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
            return Equals(input as FrameAudioQuery);
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
                hashCode = hashCode * 59 + F0.GetHashCode();

                hashCode = hashCode * 59 + Volume.GetHashCode();

                hashCode = hashCode * 59 + Phonemes.GetHashCode();

                hashCode = hashCode * 59 + VolumeScale.GetHashCode();
                hashCode = hashCode * 59 + OutputSamplingRate.GetHashCode();
                hashCode = hashCode * 59 + OutputStereo.GetHashCode();
                return hashCode;
            }
        }
    }
}