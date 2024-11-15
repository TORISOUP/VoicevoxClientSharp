using System;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace VoicevoxClientSharp.Models
{
    /// <summary>
    /// 音素の情報
    /// </summary>
    [DataContract(Name = "FramePhoneme")]
    public sealed class FramePhoneme : IEquatable<FramePhoneme>
    {
        [JsonConstructor]
        public FramePhoneme()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FramePhoneme" /> class.
        /// </summary>
        /// <param name="phoneme">音素 (required).</param>
        /// <param name="frameLength">音素のフレーム長 (required).</param>
        /// <param name="noteId">noteId.</param>
        public FramePhoneme(string phoneme, int frameLength, string? noteId)
        {
            Phoneme = phoneme;
            FrameLength = frameLength;
            NoteId = noteId;
        }

        /// <summary>
        /// 音素
        /// </summary>
        /// <value>音素</value>
        [JsonPropertyName("phoneme")]
        public string Phoneme { get; set; }

        /// <summary>
        /// 音素のフレーム長
        /// </summary>
        /// <value>音素のフレーム長</value>
        [JsonPropertyName("frame_length")]
        public int FrameLength { get; set; }

        [JsonPropertyName("note_id")] public string? NoteId { get; set; }

        /// <summary>
        /// Returns true if FramePhoneme instances are equal
        /// </summary>
        /// <param name="input">Instance of FramePhoneme to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(FramePhoneme input)
        {
            if (input == null)
            {
                return false;
            }

            return
                (
                    Phoneme == input.Phoneme ||
                    (Phoneme != null &&
                     Phoneme.Equals(input.Phoneme))
                ) &&
                (
                    FrameLength == input.FrameLength ||
                    FrameLength.Equals(input.FrameLength)
                ) &&
                (
                    NoteId == input.NoteId ||
                    (NoteId != null &&
                     NoteId.Equals(input.NoteId))
                );
        }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class FramePhoneme {\n");
            sb.Append("  Phoneme: ").Append(Phoneme).Append("\n");
            sb.Append("  FrameLength: ").Append(FrameLength).Append("\n");
            sb.Append("  NoteId: ").Append(NoteId).Append("\n");
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
            return Equals(input as FramePhoneme);
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
                if (Phoneme != null)
                {
                    hashCode = hashCode * 59 + Phoneme.GetHashCode();
                }

                hashCode = hashCode * 59 + FrameLength.GetHashCode();
                if (NoteId != null)
                {
                    hashCode = hashCode * 59 + NoteId.GetHashCode();
                }

                return hashCode;
            }
        }
    }
}