using System;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;


namespace VoicevoxClientSharp.Models
{
    /// <summary>
    /// モーラ（子音＋母音）ごとの情報
    /// </summary>
    [DataContract(Name = "Mora")]
    public sealed class Mora : IEquatable<Mora>
    {
        /// <summary>
        /// 文字
        /// </summary>
        /// <value>文字</value>
        public string Text { get; set; }

        /// <summary>
        /// 子音の音素
        /// </summary>
        /// <value>子音の音素</value>
        public string? Consonant { get; set; }

        /// <summary>
        /// 子音の音長
        /// </summary>
        /// <value>子音の音長</value>
        public decimal? ConsonantLength { get; set; }

        /// <summary>
        /// 母音の音素
        /// </summary>
        /// <value>母音の音素</value>
        public string Vowel { get; set; }

        /// <summary>
        /// 母音の音長
        /// </summary>
        /// <value>母音の音長</value>
        public decimal VowelLength { get; set; }

        /// <summary>
        /// 音高
        /// </summary>
        /// <value>音高</value>
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