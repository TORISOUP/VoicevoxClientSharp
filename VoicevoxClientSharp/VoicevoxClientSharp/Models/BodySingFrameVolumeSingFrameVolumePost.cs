using System;
using System.Runtime.Serialization;
using System.Text;


namespace VoicevoxClientSharp.Models
{
    /// <summary>
    /// BodySingFrameVolumeSingFrameVolumePost
    /// </summary>
    [DataContract(Name = "Body_sing_frame_volume_sing_frame_volume_post")]
    public sealed class BodySingFrameVolumeSingFrameVolumePost : IEquatable<BodySingFrameVolumeSingFrameVolumePost>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BodySingFrameVolumeSingFrameVolumePost" /> class.
        /// </summary>
        /// <param name="score">score (required).</param>
        /// <param name="frameAudioQuery">frameAudioQuery (required).</param>
        public BodySingFrameVolumeSingFrameVolumePost(Score score, FrameAudioQuery frameAudioQuery)
        {
            Score = score;
            FrameAudioQuery = frameAudioQuery;
        }

        /// <summary>
        /// Gets or Sets Score
        /// </summary>
        [DataMember(Name = "score", IsRequired = true, EmitDefaultValue = false)]
        public Score Score { get; set; }

        /// <summary>
        /// Gets or Sets FrameAudioQuery
        /// </summary>
        [DataMember(Name = "frame_audio_query", IsRequired = true, EmitDefaultValue = false)]
        public FrameAudioQuery FrameAudioQuery { get; set; }

        /// <summary>
        /// Returns true if BodySingFrameVolumeSingFrameVolumePost instances are equal
        /// </summary>
        /// <param name="input">Instance of BodySingFrameVolumeSingFrameVolumePost to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(BodySingFrameVolumeSingFrameVolumePost? input)
        {
            return
                input != null &&
                (
                    Score.Equals(input.Score) ||
                    Score.Equals(input.Score)
                ) &&
                (
                    FrameAudioQuery.Equals(input.FrameAudioQuery) ||
                    FrameAudioQuery.Equals(input.FrameAudioQuery)
                );
        }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class BodySingFrameVolumeSingFrameVolumePost {\n");
            sb.Append("  Score: ").Append(Score).Append("\n");
            sb.Append("  FrameAudioQuery: ").Append(FrameAudioQuery).Append("\n");
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
            return Equals(input as BodySingFrameVolumeSingFrameVolumePost);
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
                hashCode = hashCode * 59 + Score.GetHashCode();
                hashCode = hashCode * 59 + FrameAudioQuery.GetHashCode();
                return hashCode;
            }
        }
    }
}