using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;


namespace VoicevoxClientSharp.Models
{
    /// <summary>
    /// 楽譜情報
    /// </summary>
    [DataContract(Name = "Score")]
    public sealed class Score : IEquatable<Score>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Score" /> class.
        /// </summary>
        /// <param name="notes">notes (required).</param>
        public Score(List<Note> notes)
        {
            Notes = notes;
        }

        /// <summary>
        /// Gets or Sets Notes
        /// </summary>
        [DataMember(Name = "notes", IsRequired = true, EmitDefaultValue = false)]
        public List<Note> Notes { get; set; }

        /// <summary>
        /// Returns true if Score instances are equal
        /// </summary>
        /// <param name="input">Instance of Score to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Score? input)
        {
            if (input == null)
            {
                return false;
            }

            return
                Notes == input.Notes ||
                Notes.SequenceEqual(input.Notes);
        }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Score {\n");
            sb.Append("  Notes: ").Append(Notes).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }



        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object? input)
        {
            return Equals(input as Score);
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
                hashCode = hashCode * 59 + Notes.GetHashCode();

                return hashCode;
            }
        }
    }
}