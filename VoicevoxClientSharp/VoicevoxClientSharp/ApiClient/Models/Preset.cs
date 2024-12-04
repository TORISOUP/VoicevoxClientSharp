using System;
using System.Text;
using System.Text.Json.Serialization;

namespace VoicevoxClientSharp.ApiClient.Models
{
    /// <summary>
    /// プリセット情報
    /// </summary>
    public sealed class Preset : IEquatable<Preset>
    {
        /// <summary>
        /// プリセット情報
        /// </summary>
        /// <param name="id">プリセットID</param>
        /// <param name="name">プリセット名</param>
        /// <param name="speakerUuid">キャラクターのUUID</param>
        /// <param name="styleId">スタイルID</param>
        /// <param name="speedScale">全体の話速</param>
        /// <param name="pitchScale">全体の音高</param>
        /// <param name="intonationScale">全体の抑揚</param>
        /// <param name="volumeScale">全体の音量</param>
        /// <param name="prePhonemeLength">音声の前の無音時間</param>
        /// <param name="postPhonemeLength">音声の後の無音時間</param>
        /// <param name="pauseLength">句読点などの無音時間</param>
        /// <param name="pauseLengthScale">句読点などの無音時間（倍率）</param>
        public Preset(int id,
            string name,
            string speakerUuid,
            int styleId,
            decimal speedScale,
            decimal pitchScale,
            decimal intonationScale,
            decimal volumeScale,
            decimal prePhonemeLength,
            decimal postPhonemeLength,
            decimal? pauseLength = null,
            decimal? pauseLengthScale = null)
        {
            Id = id;
            Name = name;
            SpeakerUuid = speakerUuid;
            StyleId = styleId;
            SpeedScale = speedScale;
            PitchScale = pitchScale;
            IntonationScale = intonationScale;
            VolumeScale = volumeScale;
            PrePhonemeLength = prePhonemeLength;
            PostPhonemeLength = postPhonemeLength;
            PauseLength = pauseLength;
            PauseLengthScale = pauseLengthScale;
        }

        /// <summary>
        /// プリセットID
        /// </summary>
        /// <value>プリセットID</value>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// プリセット名
        /// </summary>
        /// <value>プリセット名</value>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// キャラクターのUUID
        /// </summary>
        /// <value>キャラクターのUUID</value>
        [JsonPropertyName("speaker_uuid")]
        public string SpeakerUuid { get; set; }

        /// <summary>
        /// スタイルID
        /// </summary>
        /// <value>スタイルID</value>
        [JsonPropertyName("style_id")]
        public int StyleId { get; set; }

        /// <summary>
        /// 全体の話速
        /// </summary>
        /// <value>全体の話速</value>
        [JsonPropertyName("speedScale")]
        public decimal SpeedScale { get; set; }

        /// <summary>
        /// 全体の音高
        /// </summary>
        /// <value>全体の音高</value>
        [JsonPropertyName("pitchScale")]
        public decimal PitchScale { get; set; }

        /// <summary>
        /// 全体の抑揚
        /// </summary>
        /// <value>全体の抑揚</value>
        [JsonPropertyName("intonationScale")]
        public decimal IntonationScale { get; set; }

        /// <summary>
        /// 全体の音量
        /// </summary>
        /// <value>全体の音量</value>
        [JsonPropertyName("volumeScale")]
        public decimal VolumeScale { get; set; }

        /// <summary>
        /// 音声の前の無音時間
        /// </summary>
        /// <value>音声の前の無音時間</value>
        [JsonPropertyName("prePhonemeLength")]
        public decimal PrePhonemeLength { get; set; }

        /// <summary>
        /// 音声の後の無音時間
        /// </summary>
        /// <value>音声の後の無音時間</value>
        [JsonPropertyName("postPhonemeLength")]
        public decimal PostPhonemeLength { get; set; }

        /// <summary>
        /// 句読点などの無音時間
        /// </summary>
        /// <value>句読点などの無音時間</value>
        [JsonPropertyName("pauseLength")]
        public decimal? PauseLength { get; set; }

        /// <summary>
        /// 句読点などの無音時間（倍率）
        /// </summary>
        /// <value>句読点などの無音時間（倍率）</value>
        [JsonPropertyName("pauseLengthScale")]
        public decimal? PauseLengthScale { get; set; }

        public bool Equals(Preset? other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Id == other.Id && Name == other.Name && SpeakerUuid == other.SpeakerUuid &&
                   StyleId == other.StyleId && SpeedScale == other.SpeedScale && PitchScale == other.PitchScale &&
                   IntonationScale == other.IntonationScale && VolumeScale == other.VolumeScale &&
                   PrePhonemeLength == other.PrePhonemeLength && PostPhonemeLength == other.PostPhonemeLength &&
                   PauseLength == other.PauseLength && PauseLengthScale == other.PauseLengthScale;
        }


        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Preset {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  SpeakerUuid: ").Append(SpeakerUuid).Append("\n");
            sb.Append("  StyleId: ").Append(StyleId).Append("\n");
            sb.Append("  SpeedScale: ").Append(SpeedScale).Append("\n");
            sb.Append("  PitchScale: ").Append(PitchScale).Append("\n");
            sb.Append("  IntonationScale: ").Append(IntonationScale).Append("\n");
            sb.Append("  VolumeScale: ").Append(VolumeScale).Append("\n");
            sb.Append("  PrePhonemeLength: ").Append(PrePhonemeLength).Append("\n");
            sb.Append("  PostPhonemeLength: ").Append(PostPhonemeLength).Append("\n");
            sb.Append("  PauseLength: ").Append(PauseLength).Append("\n");
            sb.Append("  PauseLengthScale: ").Append(PauseLengthScale).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }


        public override bool Equals(object? obj)
        {
            return ReferenceEquals(this, obj) || (obj is Preset other && Equals(other));
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id;
                hashCode = (hashCode * 397) ^ Name.GetHashCode();
                hashCode = (hashCode * 397) ^ SpeakerUuid.GetHashCode();
                hashCode = (hashCode * 397) ^ StyleId;
                hashCode = (hashCode * 397) ^ SpeedScale.GetHashCode();
                hashCode = (hashCode * 397) ^ PitchScale.GetHashCode();
                hashCode = (hashCode * 397) ^ IntonationScale.GetHashCode();
                hashCode = (hashCode * 397) ^ VolumeScale.GetHashCode();
                hashCode = (hashCode * 397) ^ PrePhonemeLength.GetHashCode();
                hashCode = (hashCode * 397) ^ PostPhonemeLength.GetHashCode();
                hashCode = (hashCode * 397) ^ PauseLength.GetHashCode();
                hashCode = (hashCode * 397) ^ PauseLengthScale.GetHashCode();
                return hashCode;
            }
        }
    }
}