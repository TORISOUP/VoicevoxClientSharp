using System;
using UnityEngine;

namespace VoicevoxClientSharp.Unity.Utilities
{
    public static class AudioUtility
    {
        public static AudioClip CreateAudioClipFromWav(byte[] wavData)
        {
            var headerOffset = 44; // WAVの標準ヘッダーサイズ
            var sampleCount = (wavData.Length - headerOffset) / 2; // 16ビット (2バイト) サンプル

            var frequency = BitConverter.ToInt32(wavData, 24); // サンプリング周波数を取得
            var channels = BitConverter.ToInt16(wavData, 22); // チャンネル数を取得

            // 音声データをfloat配列に変換
            var audioData = new float[sampleCount];
            for (var i = 0; i < sampleCount; i++)
            {
                var sample = BitConverter.ToInt16(wavData, headerOffset + i * 2);
                audioData[i] = sample / 32768f; // 16ビットの範囲をfloat (-1.0 ~ 1.0) に変換
            }

            var audioClip = AudioClip.Create("GeneratedAudio", sampleCount, channels, frequency, false);
            audioClip.SetData(audioData, 0);

            return audioClip;
        }
    }
}