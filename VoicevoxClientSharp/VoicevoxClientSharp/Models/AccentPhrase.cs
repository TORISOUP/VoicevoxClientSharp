using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace VoicevoxClientSharp.Models
{
    /// <summary>
    /// アクセント句ごとの情報
    /// </summary>
    [DataContract(Name = "AccentPhrase")]
    public sealed class AccentPhrase : IEquatable<AccentPhrase>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccentPhrase" /> class.
        /// </summary>
        /// <param name="moras">moras (required).</param>
        /// <param name="accent">アクセント箇所 (required).</param>
        /// <param name="pauseMora">pauseMora.</param>
        /// <param name="isInterrogative">疑問系かどうか (default to false).</param>
        public AccentPhrase(List<Mora> moras,
            int accent,
            Mora? pauseMora,
            bool isInterrogative = false)
        {
            Moras = moras;
            Accent = accent;
            PauseMora = pauseMora;
            IsInterrogative = isInterrogative;
        }

        /// <summary>
        /// Gets or Sets Moras
        /// </summary>
        [DataMember(Name = "moras", IsRequired = true, EmitDefaultValue = false)]
        public List<Mora> Moras { get; set; }

        /// <summary>
        /// アクセント箇所
        /// </summary>
        /// <value>アクセント箇所</value>
        [DataMember(Name = "accent", IsRequired = true, EmitDefaultValue = false)]
        public int Accent { get; set; }

        /// <summary>
        /// Gets or Sets PauseMora
        /// </summary>
        [DataMember(Name = "pause_mora", EmitDefaultValue = true)]
        public Mora? PauseMora { get; set; }

        /// <summary>
        /// 疑問系かどうか
        /// </summary>
        /// <value>疑問系かどうか</value>
        [DataMember(Name = "is_interrogative", EmitDefaultValue = true)]
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

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}