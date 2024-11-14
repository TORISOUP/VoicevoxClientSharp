using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VoicevoxClientSharp.Models;

namespace VoicevoxClientSharp.ApiClient
{
    public interface ISynthesisClient
    {
        /// <summary>
        /// POST /synthesis
        /// 音声合成する
        /// </summary>
        /// <param name="speakerId"></param>
        /// <param name="audioQuery"></param>
        /// <param name="enableInterrogativeUpspeak">疑問系のテキストが与えられたら語尾を自動調整する</param>
        /// <param name="coreVersion"></param>
        /// <param name="ct"></param>
        /// <returns>wav</returns>
        ValueTask<byte[]> SynthesisAsync(int speakerId,
            AudioQuery audioQuery,
            bool? enableInterrogativeUpspeak = true,
            string? coreVersion = null,
            CancellationToken ct = default);

        /// <summary>
        /// POST /cancellable_synthesis
        /// 音声合成する（キャンセル可能）
        /// この API は実験的機能であり、エンジン起動時に引数で--enable_cancellable_synthesisを指定しないと有効化されません。
        /// </summary>
        /// <param name="speakerId"></param>
        /// <param name="audioQuery"></param>
        /// <param name="enableInterrogativeUpspeak">疑問系のテキストが与えられたら語尾を自動調整する</param>
        /// <param name="coreVersion"></param>
        /// <param name="ct"></param>
        /// <returns>wav</returns>
        ValueTask<byte[]> CancellableSynthesisAsync(int speakerId,
            AudioQuery audioQuery,
            bool? enableInterrogativeUpspeak = true,
            string? coreVersion = null,
            CancellationToken ct = default);

        /// <summary>
        /// POST /multi_synthesis
        /// 複数まとめて音声合成する
        /// </summary>
        /// <param name="speakerId"></param>
        /// <param name="audioQueries"></param>
        /// <param name="coreVersion"></param>
        /// <param name="ct"></param>
        /// <returns>zip圧縮されたwavファイル群</returns>
        ValueTask<byte[]> MultiSpeakerSynthesisAsync(int speakerId,
            AudioQuery[] audioQueries,
            string? coreVersion = null,
            CancellationToken ct = default);

        /// <summary>
        /// POST /frame_synthesis
        /// 歌唱音声合成を行います。
        /// </summary>
        /// <param name="speakerId"></param>
        /// <param name="frameAudioQuery"></param>
        /// <param name="coreVersion"></param>
        /// <param name="ct"></param>
        /// <returns>wav</returns>
        ValueTask<byte[]> FrameSynthesisAsync(int speakerId,
            FrameAudioQuery frameAudioQuery,
            string? coreVersion = null,
            CancellationToken ct = default);

        /// <summary>
        /// POST /morphable_targets
        /// 指定したスタイルに対してエンジン内のキャラクターがモーフィングが可能か判定する
        /// 
        /// 指定されたベーススタイルに対してエンジン内の各キャラクターがモーフィング機能を利用可能か返します。
        /// モーフィングの許可/禁止は/speakersのspeaker.supported_features.synthesis_morphingに記載されています。
        /// プロパティが存在しない場合は、モーフィングが許可されているとみなします。
        /// 返り値のスタイルIDはstring型なので注意。
        /// </summary>
        /// <param name="speakerIds"></param>
        /// <param name="coreVersion"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        ValueTask<IReadOnlyDictionary<int, MorphableTargetInfo>[]> IsMorphableTargetsAsync(
            int[] speakerIds,
            string? coreVersion = null,
            CancellationToken ct = default);

        /// <summary>
        /// POST /synthesis_morphing
        /// 2種類のスタイルでモーフィングした音声を合成する
        ///
        /// 指定された2種類のスタイルで音声を合成、指定した割合でモーフィングした音声を得ます。
        /// モーフィングの割合はmorph_rateで指定でき、0.0でベースのスタイル、1.0でターゲットのスタイルに近づきます。
        /// </summary>
        /// <param name="baseSpeakerId"></param>
        /// <param name="targetSpeakerId"></param>
        /// <param name="morphRate"></param>
        /// <param name="audioQuery"></param>
        /// <param name="coreVersion"></param>
        /// <param name="ct"></param>
        /// <returns>wav</returns>
        ValueTask<byte[]> SynthesisMorphingAsync(
            int baseSpeakerId,
            int targetSpeakerId,
            decimal morphRate,
            AudioQuery audioQuery,
            string? coreVersion = null,
            CancellationToken ct = default);
    }

    public partial class RawApiClient
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public ValueTask<byte[]> SynthesisAsync(int speakerId,
            AudioQuery audioQuery,
            bool? enableInterrogativeUpspeak,
            string? coreVersion = null,
            CancellationToken ct = default)
        {
            var queryString = QueryString(
                ("speaker", speakerId.ToString()),
                ("core_version", coreVersion),
                ("enable_interrogative_upspeak", enableInterrogativeUpspeak?.ToString())
            );
            var url = $"{_baseUrl}/synthesis?{queryString}";
            return PostAndByteResponseAsync(url, audioQuery, ct);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public ValueTask<byte[]> CancellableSynthesisAsync(int speakerId,
            AudioQuery audioQuery,
            bool? enableInterrogativeUpspeak,
            string? coreVersion = null,
            CancellationToken ct = default)
        {
            var queryString = QueryString(
                ("speaker", speakerId.ToString()),
                ("core_version", coreVersion),
                ("enable_interrogative_upspeak", enableInterrogativeUpspeak?.ToString())
            );
            var url = $"{_baseUrl}/cancellable_synthesis?{queryString}";
            return PostAndByteResponseAsync(url, audioQuery, ct);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public ValueTask<byte[]> MultiSpeakerSynthesisAsync(int speakerId,
            AudioQuery[] audioQueries,
            string? coreVersion = null,
            CancellationToken ct = default)
        {
            var queryString = QueryString(
                ("speaker", speakerId.ToString()),
                ("core_version", coreVersion)
            );
            var url = $"{_baseUrl}/multi_synthesis?{queryString}";
            return PostAndByteResponseAsync(url, audioQueries, ct);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public ValueTask<byte[]> FrameSynthesisAsync(int speakerId,
            FrameAudioQuery frameAudioQuery,
            string? coreVersion = null,
            CancellationToken ct = default)
        {
            var queryString = QueryString(
                ("speaker", speakerId.ToString()),
                ("core_version", coreVersion)
            );
            var url = $"{_baseUrl}/frame_synthesis?{queryString}";
            return PostAndByteResponseAsync(url, frameAudioQuery, ct);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async ValueTask<IReadOnlyDictionary<int, MorphableTargetInfo>[]> IsMorphableTargetsAsync(
            int[] speakerIds,
            string? coreVersion = null,
            CancellationToken ct = default)
        {
            var queryString = QueryString(("core_version", coreVersion));
            var url = $"{_baseUrl}/morphable_targets?{queryString}";
            var result = await PostAsync<int[], Dictionary<string, MorphableTargetInfo>[]>(url, speakerIds, ct);
            return result.Select(x =>
                {
                    return (IReadOnlyDictionary<int, MorphableTargetInfo>)x.ToDictionary(
                        s => int.Parse(s.Key),
                        s => s.Value
                    );
                })
                .ToArray();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public ValueTask<byte[]> SynthesisMorphingAsync(
            int baseSpeakerId,
            int targetSpeakerId,
            decimal morphRate,
            AudioQuery audioQuery,
            string? coreVersion = null,
            CancellationToken ct = default)
        {
            morphRate = Math.Min(Math.Max(morphRate, 0M), 1.0M);

            var queryString = QueryString(
                ("base_speaker", baseSpeakerId.ToString()),
                ("target_speaker", targetSpeakerId.ToString()),
                ("morph_rate", morphRate.ToString()),
                ("core_version", coreVersion)
            );
            
            var url = $"{_baseUrl}/synthesis_morphing?{queryString}";
            return PostAndByteResponseAsync(url, audioQuery, ct);
        }
    }
}