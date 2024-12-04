using System;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace VoicevoxClientSharp.ApiClient.Models
{
    /// <summary>
    /// 音符ごとの情報
    /// </summary>
    [DataContract(Name = "Note")]
    public sealed class Note : IEquatable<Note>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Note" /> class.
        /// </summary>
        /// <param name="id">id.</param>
        /// <param name="key">音階.</param>
        /// <param name="frameLength">音符のフレーム長 (required).</param>
        /// <param name="lyric">音符の歌詞 (required).</param>
        public Note(string? id, int? key, int frameLength, string lyric)
        {
            FrameLength = frameLength;
            Lyric = lyric;
            Id = id;
            Key = key;
        }

        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        /// <summary>
        /// 音階
        /// </summary>
        /// <value>音階</value>
        [JsonPropertyName("key")]
        public int? Key { get; set; }

        /// <summary>
        /// 音符のフレーム長
        /// </summary>
        /// <value>音符のフレーム長</value>
        [JsonPropertyName("frame_length")]
        public int FrameLength { get; set; }

        /// <summary>
        /// 音符の歌詞
        /// </summary>
        /// <value>音符の歌詞</value>
        [JsonPropertyName("lyric")]
        public string Lyric { get; set; }

        public bool Equals(Note? other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Id == other.Id && Key == other.Key && FrameLength == other.FrameLength && Lyric == other.Lyric;
        }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Note {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Key: ").Append(Key).Append("\n");
            sb.Append("  FrameLength: ").Append(FrameLength).Append("\n");
            sb.Append("  Lyric: ").Append(Lyric).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }


        public override bool Equals(object? obj)
        {
            return ReferenceEquals(this, obj) || (obj is Note other && Equals(other));
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id != null ? Id.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ Key.GetHashCode();
                hashCode = (hashCode * 397) ^ FrameLength;
                hashCode = (hashCode * 397) ^ Lyric.GetHashCode();
                return hashCode;
            }
        }
    }
}