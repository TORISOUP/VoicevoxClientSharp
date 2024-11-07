using System;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace VoicevoxClientSharp.Models
{
    /// <summary>
    /// 音声ライブラリに含まれるキャラクターの情報
    /// </summary>
    [DataContract(Name = "LibrarySpeaker")]
    public sealed class LibrarySpeaker : IEquatable<LibrarySpeaker>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LibrarySpeaker" /> class.
        /// </summary>
        /// <param name="speaker">speaker (required).</param>
        /// <param name="speakerInfo">speakerInfo (required).</param>
        public LibrarySpeaker(Speaker speaker, SpeakerInfo speakerInfo)
        {
            Speaker = speaker;
            SpeakerInfo = speakerInfo;
        }

        /// <summary>
        /// Gets or Sets Speaker
        /// </summary>
        [DataMember(Name = "speaker", IsRequired = true, EmitDefaultValue = false)]
        public Speaker Speaker { get; set; }

        /// <summary>
        /// Gets or Sets SpeakerInfo
        /// </summary>
        [DataMember(Name = "speaker_info", IsRequired = true, EmitDefaultValue = false)]
        public SpeakerInfo SpeakerInfo { get; set; }

        /// <summary>
        /// Returns true if LibrarySpeaker instances are equal
        /// </summary>
        /// <param name="input">Instance of LibrarySpeaker to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(LibrarySpeaker? input)
        {
            if (input == null)
            {
                return false;
            }

            return
                (
                    Speaker.Equals(input.Speaker) ||
                    Speaker.Equals(input.Speaker)
                ) &&
                (
                    SpeakerInfo.Equals(input.SpeakerInfo) ||
                    SpeakerInfo.Equals(input.SpeakerInfo)
                );
        }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class LibrarySpeaker {\n");
            sb.Append("  Speaker: ").Append(Speaker).Append("\n");
            sb.Append("  SpeakerInfo: ").Append(SpeakerInfo).Append("\n");
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
            return Equals(input as LibrarySpeaker);
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
                hashCode = hashCode * 59 + Speaker.GetHashCode();
                hashCode = hashCode * 59 + SpeakerInfo.GetHashCode();
                return hashCode;
            }
        }
    }
}