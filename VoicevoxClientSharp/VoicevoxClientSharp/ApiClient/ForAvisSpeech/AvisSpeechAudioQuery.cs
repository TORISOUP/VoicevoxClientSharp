using System;
using System.Text.Json.Serialization;
using VoicevoxClientSharp.ApiClient.Models;

namespace VoicevoxClientSharp.ApiClient.ForAvisSpeech
{
    /// <summary>
    /// AvisSpeechの音声合成用のクエリ
    /// </summary>
    public class AvisSpeechAudioQuery
    {
        [JsonPropertyName("accent_phrases")] public AccentPhrase[] AccentPhrases { get; set; } = Array.Empty<AccentPhrase>();

        [JsonPropertyName("speedScale")] public double SpeedScale { get; set; }

        [JsonPropertyName("intonationScale")] public double IntonationScale { get; set; }

        [JsonPropertyName("tempoDynamicsScale")]
        public double TempoDynamicsScale { get; set; } = 1.0;

        [JsonPropertyName("pitchScale")] public double PitchScale { get; set; }

        [JsonPropertyName("volumeScale")] public double VolumeScale { get; set; }

        [JsonPropertyName("prePhonemeLength")] public double PrePhonemeLength { get; set; }

        [JsonPropertyName("postPhonemeLength")]
        public double PostPhonemeLength { get; set; }

        [JsonPropertyName("pauseLength")] public double? PauseLength { get; set; }

        [JsonPropertyName("pauseLengthScale")] public double PauseLengthScale { get; set; } = 1.0;

        [JsonPropertyName("outputSamplingRate")]
        public int OutputSamplingRate { get; set; }

        [JsonPropertyName("outputStereo")] public bool OutputStereo { get; set; }

        [JsonPropertyName("kana")] public string Kana { get; set; }
    }
}