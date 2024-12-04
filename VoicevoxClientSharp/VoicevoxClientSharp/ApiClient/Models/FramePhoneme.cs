using System;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace VoicevoxClientSharp.ApiClient.Models
{
    /// <summary>
    /// 音素の情報
    /// </summary>
    [DataContract(Name = "FramePhoneme")]
    public sealed class FramePhoneme : IEquatable<FramePhoneme>
    {
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

        public bool Equals(FramePhoneme? other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Phoneme == other.Phoneme && FrameLength == other.FrameLength && NoteId == other.NoteId;
        }

        public override bool Equals(object? obj)
        {
            return ReferenceEquals(this, obj) || obj is FramePhoneme other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Phoneme.GetHashCode();
                hashCode = (hashCode * 397) ^ FrameLength;
                hashCode = (hashCode * 397) ^ (NoteId != null ? NoteId.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}