using System;
using System.Threading;
using System.Threading.Tasks;
using VoicevoxClientSharp.ApiClient.Models;

namespace VoicevoxClientSharp.ApiClient
{
    public interface ISingClient
    {
        #region Query

        /// <summary>
        /// POST /sing_frame_audio_query
        /// 歌唱音声合成用のクエリを作成する
        /// </summary>
        ValueTask<FrameAudioQuery> CreateSingFrameAudioQueryAsync(Score score,
            int speakerId,
            string? coreVersion = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// POST /sing_frame_volume
        /// 楽譜・歌唱音声合成用のクエリからフレームごとの音量を得る
        /// </summary>
        ValueTask<decimal[]> FetchSingFrameVolumeAsync(
            int speakerId,
            Score score,
            FrameAudioQuery frameAudioQuery,
            string? coreVersion = null,
            CancellationToken cancellationToken = default);

        #endregion

        #region Sing

        /// <summary>
        /// POST /frame_synthesis
        /// 歌唱音声合成を行います。
        /// </summary>
        /// <param name="speakerId"></param>
        /// <param name="frameAudioQuery"></param>
        /// <param name="coreVersion"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>wav</returns>
        ValueTask<byte[]> FrameSynthesisAsync(int speakerId,
            FrameAudioQuery frameAudioQuery,
            string? coreVersion = null,
            CancellationToken cancellationToken = default);

        #endregion

        #region Singer

        /// <summary>
        /// GET /singers
        /// 歌えるキャラクターの情報の一覧を返します。
        /// </summary>
        /// <param name="coreVersion"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        ValueTask<Speaker[]> GetSingersAsync(string? coreVersion = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// GET /singer_info
        /// UUID で指定された歌えるキャラクターの情報を返します。 画像や音声はresourceFormatで指定した形式で返されます。
        /// </summary>
        /// <param name="speakerUuId"></param>
        /// <param name="resourceFormat"></param>
        /// <param name="coreVersion"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        ValueTask<SpeakerInfo> GetSingerInfoAsync(
            string speakerUuId,
            ResourceFormat? resourceFormat = ResourceFormat.Base64,
            string? coreVersion = null,
            CancellationToken cancellationToken = default);

        #endregion
    }

    public partial class VoicevoxApiClient
    {
        /// <summary>
        ///     <inheritdoc />
        /// </summary>
        public ValueTask<FrameAudioQuery> CreateSingFrameAudioQueryAsync(Score score,
            int speakerId,
            string? coreVersion = null,
            CancellationToken cancellationToken = default)
        {
            var queryString = CreateQueryString(
                ("speaker", speakerId.ToString()),
                ("core_version", coreVersion)
            );

            var url = $"{_baseUrl}/sing_frame_audio_query?{queryString}";
            return PostAsync<Score, FrameAudioQuery>(url, score, cancellationToken);
        }


        /// <summary>
        ///     <inheritdoc />
        /// </summary>
        public ValueTask<decimal[]> FetchSingFrameVolumeAsync(int speakerId,
            Score score,
            FrameAudioQuery frameAudioQuery,
            string? coreVersion = null,
            CancellationToken cancellationToken = default)
        {
            var request = new SingFrameVolumeRequest(score, frameAudioQuery);
            var queryString = CreateQueryString(
                ("speaker", speakerId.ToString()),
                ("core_version", coreVersion)
            );

            var url = $"{_baseUrl}/sing_frame_volume?{queryString}";
            return PostAsync<SingFrameVolumeRequest, decimal[]>(url, request, cancellationToken);
        }


        /// <summary>
        ///     <inheritdoc />
        /// </summary>
        public ValueTask<byte[]> FrameSynthesisAsync(int speakerId,
            FrameAudioQuery frameAudioQuery,
            string? coreVersion = null,
            CancellationToken cancellationToken = default)
        {
            var queryString = CreateQueryString(
                ("speaker", speakerId.ToString()),
                ("core_version", coreVersion)
            );
            var url = $"{_baseUrl}/frame_synthesis?{queryString}";
            return PostAndByteResponseAsync(url, frameAudioQuery, cancellationToken);
        }


        /// <summary>
        ///     <inheritdoc />
        /// </summary>
        public ValueTask<Speaker[]> GetSingersAsync(string? coreVersion = null, CancellationToken cancellationToken = default)
        {
            var queryString = CreateQueryString(
                ("core_version", coreVersion)
            );

            var url = $"{_baseUrl}/singers?{queryString}";
            return GetAsync<Speaker[]>(url, cancellationToken);
        }

        /// <summary>
        ///     <inheritdoc />
        /// </summary>
        public ValueTask<SpeakerInfo> GetSingerInfoAsync(string speakerUuId,
            ResourceFormat? resourceFormat,
            string? coreVersion = null,
            CancellationToken cancellationToken = default)
        {
            var resourceFormatStr = resourceFormat switch
            {
                ResourceFormat.Base64 => "base64",
                ResourceFormat.Url => "url",
                null => "base64",
                _ => throw new ArgumentOutOfRangeException(nameof(resourceFormat), resourceFormat, null)
            };

            var queryString = CreateQueryString(
                ("speaker_uuid", speakerUuId),
                ("resource_format", resourceFormatStr),
                ("core_version", coreVersion)
            );

            var url = $"{_baseUrl}/singer_info?{queryString}";
            return GetAsync<SpeakerInfo>(url, cancellationToken);
        }
    }
}