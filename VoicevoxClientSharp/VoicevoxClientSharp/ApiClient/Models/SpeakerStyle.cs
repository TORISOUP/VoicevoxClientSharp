using System;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace VoicevoxClientSharp.ApiClient.Models
{
    /// <summary>
    /// キャラクターのスタイル情報
    /// </summary>
    [DataContract(Name = "SpeakerStyle")]
    public sealed class SpeakerStyle : IEquatable<SpeakerStyle>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpeakerStyle" /> class.
        /// </summary>
        /// <param name="name">スタイル名 (required).</param>
        /// <param name="id">スタイルID (required).</param>
        /// <param name="type">
        /// スタイルの種類。talk:音声合成クエリの作成と音声合成が可能。singing_teacher:歌唱音声合成用のクエリの作成が可能。frame_decode:歌唱音声合成が可能。sing:歌唱音声合成用のクエリの作成と歌唱音声合成が可能。
        /// (default to TypeEnum.Talk).
        /// </param>
        public SpeakerStyle(string name, int id, SpeakerType? type = SpeakerType.Talk)
        {
            Name = name;
            Id = id;
            Type = type;
        }

        /// <summary>
        /// スタイルの種類。talk:音声合成クエリの作成と音声合成が可能。singing_teacher:歌唱音声合成用のクエリの作成が可能。frame_decode:歌唱音声合成が可能。sing:歌唱音声合成用のクエリの作成と歌唱音声合成が可能。
        /// </summary>
        /// <value>スタイルの種類。talk:音声合成クエリの作成と音声合成が可能。singing_teacher:歌唱音声合成用のクエリの作成が可能。frame_decode:歌唱音声合成が可能。sing:歌唱音声合成用のクエリの作成と歌唱音声合成が可能。</value>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(Extensions.JsonStringEnumConverter<SpeakerType>))]
        public SpeakerType? Type { get; set; }

        /// <summary>
        /// スタイル名
        /// </summary>
        /// <value>スタイル名</value>
        [DataMember(Name = "name", IsRequired = true, EmitDefaultValue = false)]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// スタイルID
        /// </summary>
        /// <value>スタイルID</value>
        [DataMember(Name = "id", IsRequired = true, EmitDefaultValue = false)]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        public bool Equals(SpeakerStyle? other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Type == other.Type && Name == other.Name && Id == other.Id;
        }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class SpeakerStyle {\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Type: ").Append(Type).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        public override bool Equals(object? obj)
        {
            return ReferenceEquals(this, obj) || (obj is SpeakerStyle other && Equals(other));
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Type.GetHashCode();
                hashCode = (hashCode * 397) ^ Name.GetHashCode();
                hashCode = (hashCode * 397) ^ Id;
                return hashCode;
            }
        }
    }

    /// <summary>
    /// スタイルの種類。talk:音声合成クエリの作成と音声合成が可能。singing_teacher:歌唱音声合成用のクエリの作成が可能。frame_decode:歌唱音声合成が可能。sing:歌唱音声合成用のクエリの作成と歌唱音声合成が可能。
    /// </summary>
    /// <value>スタイルの種類。talk:音声合成クエリの作成と音声合成が可能。singing_teacher:歌唱音声合成用のクエリの作成が可能。frame_decode:歌唱音声合成が可能。sing:歌唱音声合成用のクエリの作成と歌唱音声合成が可能。</value>
    public enum SpeakerType
    {
        /// <summary>
        /// Enum Talk for value: talk
        /// </summary>
        [EnumMember(Value = "talk")] Talk = 1,

        /// <summary>
        /// Enum SingingTeacher for value: singing_teacher
        /// </summary>
        [EnumMember(Value = "singing_teacher")]
        SingingTeacher = 2,

        /// <summary>
        /// Enum FrameDecode for value: frame_decode
        /// </summary>
        [EnumMember(Value = "frame_decode")] FrameDecode = 3,

        /// <summary>
        /// Enum Sing for value: sing
        /// </summary>
        [EnumMember(Value = "sing")] Sing = 4
    }
}