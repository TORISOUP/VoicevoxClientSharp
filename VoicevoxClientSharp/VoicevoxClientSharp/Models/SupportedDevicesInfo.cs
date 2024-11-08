/*
 * VOICEVOX Engine
 *
 * VOICEVOX の音声合成エンジンです。
 *
 * The version of the OpenAPI document: 0.21.1
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */


using System;
using System.Runtime.Serialization;
using System.Text;


namespace VoicevoxClientSharp.Models
{
    /// <summary>
    /// 対応しているデバイスの情報
    /// </summary>
    [DataContract(Name = "SupportedDevicesInfo")]
    public sealed class SupportedDevicesInfo : IEquatable<SupportedDevicesInfo>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SupportedDevicesInfo" /> class.
        /// </summary>
        /// <param name="cpu">CPUに対応しているか (required).</param>
        /// <param name="cuda">CUDA(Nvidia GPU)に対応しているか (required).</param>
        /// <param name="dml">DirectML(Nvidia GPU/Radeon GPU等)に対応しているか (required).</param>
        public SupportedDevicesInfo(bool cpu, bool cuda, bool dml)
        {
            Cpu = cpu;
            Cuda = cuda;
            Dml = dml;
        }

        /// <summary>
        /// CPUに対応しているか
        /// </summary>
        /// <value>CPUに対応しているか</value>
        [DataMember(Name = "cpu", IsRequired = true, EmitDefaultValue = false)]
        public bool Cpu { get; set; }

        /// <summary>
        /// CUDA(Nvidia GPU)に対応しているか
        /// </summary>
        /// <value>CUDA(Nvidia GPU)に対応しているか</value>
        [DataMember(Name = "cuda", IsRequired = true, EmitDefaultValue = false)]
        public bool Cuda { get; set; }

        /// <summary>
        /// DirectML(Nvidia GPU/Radeon GPU等)に対応しているか
        /// </summary>
        /// <value>DirectML(Nvidia GPU/Radeon GPU等)に対応しているか</value>
        [DataMember(Name = "dml", IsRequired = true, EmitDefaultValue = false)]
        public bool Dml { get; set; }

        /// <summary>
        /// Returns true if SupportedDevicesInfo instances are equal
        /// </summary>
        /// <param name="input">Instance of SupportedDevicesInfo to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(SupportedDevicesInfo? input)
        {
            if (input == null)
            {
                return false;
            }

            return
                (
                    Cpu == input.Cpu ||
                    Cpu.Equals(input.Cpu)
                ) &&
                (
                    Cuda == input.Cuda ||
                    Cuda.Equals(input.Cuda)
                ) &&
                (
                    Dml == input.Dml ||
                    Dml.Equals(input.Dml)
                );
        }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class SupportedDevicesInfo {\n");
            sb.Append("  Cpu: ").Append(Cpu).Append("\n");
            sb.Append("  Cuda: ").Append(Cuda).Append("\n");
            sb.Append("  Dml: ").Append(Dml).Append("\n");
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
            return Equals(input as SupportedDevicesInfo);
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
                hashCode = hashCode * 59 + Cpu.GetHashCode();
                hashCode = hashCode * 59 + Cuda.GetHashCode();
                hashCode = hashCode * 59 + Dml.GetHashCode();
                return hashCode;
            }
        }
    }
}