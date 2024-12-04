using System;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace VoicevoxClientSharp.ApiClient.Models
{
    public sealed class SingFrameVolumeRequest : IEquatable<SingFrameVolumeRequest>
    {
        public SingFrameVolumeRequest(Score score, FrameAudioQuery frameAudioQuery)
        {
            Score = score;
            FrameAudioQuery = frameAudioQuery;
        }

        /// <summary>
        /// Gets or Sets Score
        /// </summary>
        [JsonPropertyName("score")]
        public Score Score { get; set; }

        /// <summary>
        /// Gets or Sets FrameAudioQuery
        /// </summary>
        [JsonPropertyName("frame_audio_query")]
        public FrameAudioQuery FrameAudioQuery { get; set; }

        /// <summary>
        /// Returns true if BodySingFrameVolumeSingFrameVolumePost instances are equal
        /// </summary>
        /// <param name="input">Instance of BodySingFrameVolumeSingFrameVolumePost to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(SingFrameVolumeRequest? input)
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
            return Equals(input as SingFrameVolumeRequest);
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