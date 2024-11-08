using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;


namespace VoicevoxClientSharp.Models
{
    /// <summary>
    /// アクセント句ごとの情報
    /// </summary>
    [DataContract(Name = "AccentPhrase")]
    public sealed class AccentPhrase : IEquatable<AccentPhrase>
    {
        /// <summary>
        /// Moras
        /// </summary>
        public List<Mora> Moras { get; set; }

        /// <summary>
        /// アクセント箇所
        /// </summary>
        /// <value>アクセント箇所</value>
        public int Accent { get; set; }

        /// <summary>
        /// Gets or Sets PauseMora
        /// </summary>
        public Mora? PauseMora { get; set; }

        /// <summary>
        /// 疑問系かどうか
        /// </summary>
        /// <value>疑問系かどうか</value>
        public bool? IsInterrogative { get; set; }

        public bool Equals(AccentPhrase? other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Moras.SequenceEqual(other.Moras) && Accent == other.Accent && Equals(PauseMora, other.PauseMora) &&
                   IsInterrogative == other.IsInterrogative;
        }


        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class AccentPhrase {\n");
            sb.Append("  Moras: ").Append(Moras).Append("\n");
            sb.Append("  Accent: ").Append(Accent).Append("\n");
            sb.Append("  PauseMora: ").Append(PauseMora).Append("\n");
            sb.Append("  IsInterrogative: ").Append(IsInterrogative).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }


    }
}