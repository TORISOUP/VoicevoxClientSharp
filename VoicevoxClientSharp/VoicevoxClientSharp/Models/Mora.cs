using System;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace VoicevoxClientSharp.Models
{
    /// <summary>
    /// モーラ（子音＋母音）ごとの情報
    /// </summary>
    [DataContract(Name = "Mora")]
    public sealed class Mora : IEquatable<Mora>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Mora" /> class.
        /// </summary>
        /// <param name="text">文字 (required).</param>
        /// <param name="consonant">子音の音素.</param>
        /// <param name="consonantLength">子音の音長.</param>
        /// <param name="vowel">母音の音素 (required).</param>
        /// <param name="vowelLength">母音の音長 (required).</param>
        /// <param name="pitch">音高 (required).</param>
        public Mora(string text,
            string? consonant,
            decimal? consonantLength,
            string vowel,
            decimal vowelLength,
            decimal pitch)
        {
            Text = text;
            Vowel = vowel;
            VowelLength = vowelLength;
            Pitch = pitch;
            Consonant = consonant;
            ConsonantLength = consonantLength;
        }

        /// <summary>
        /// 文字
        /// </summary>
        /// <value>文字</value>
        [DataMember(Name = "text", IsRequired = true, EmitDefaultValue = false)]
        public string Text { get; set; }

        /// <summary>
        /// 子音の音素
        /// </summary>
        /// <value>子音の音素</value>
        [DataMember(Name = "consonant", EmitDefaultValue = false)]
        public string? Consonant { get; set; }

        /// <summary>
        /// 子音の音長
        /// </summary>
        /// <value>子音の音長</value>
        [DataMember(Name = "consonant_length", EmitDefaultValue = true)]
        public decimal? ConsonantLength { get; set; }

        /// <summary>
        /// 母音の音素
        /// </summary>
        /// <value>母音の音素</value>
        [DataMember(Name = "vowel", IsRequired = true, EmitDefaultValue = false)]
        public string Vowel { get; set; }

        /// <summary>
        /// 母音の音長
        /// </summary>
        /// <value>母音の音長</value>
        [DataMember(Name = "vowel_length", IsRequired = true, EmitDefaultValue = false)]
        public decimal VowelLength { get; set; }

        /// <summary>
        /// 音高
        /// </summary>
        /// <value>音高</value>
        [DataMember(Name = "pitch", IsRequired = true, EmitDefaultValue = false)]
        public decimal Pitch { get; set; }

        public bool Equals(Mora? other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Text == other.Text && Consonant == other.Consonant && ConsonantLength == other.ConsonantLength &&
                   Vowel == other.Vowel && VowelLength == other.VowelLength && Pitch == other.Pitch;
        }


        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Mora {\n");
            sb.Append("  Text: ").Append(Text).Append("\n");
            sb.Append("  Consonant: ").Append(Consonant).Append("\n");
            sb.Append("  ConsonantLength: ").Append(ConsonantLength).Append("\n");
            sb.Append("  Vowel: ").Append(Vowel).Append("\n");
            sb.Append("  VowelLength: ").Append(VowelLength).Append("\n");
            sb.Append("  Pitch: ").Append(Pitch).Append("\n");
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

        public override bool Equals(object? obj)
        {
            return ReferenceEquals(this, obj) || (obj is Mora other && Equals(other));
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Text.GetHashCode();
                hashCode = (hashCode * 397) ^ (Consonant != null ? Consonant.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ ConsonantLength.GetHashCode();
                hashCode = (hashCode * 397) ^ Vowel.GetHashCode();
                hashCode = (hashCode * 397) ^ VowelLength.GetHashCode();
                hashCode = (hashCode * 397) ^ Pitch.GetHashCode();
                return hashCode;
            }
        }
    }
}