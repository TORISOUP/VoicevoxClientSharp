using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VoicevoxClientSharp.Unity.Utilities;

namespace VoicevoxClientSharp.Unity
{
    public sealed class VoicevoxSpeakPlayer : MonoBehaviour
    {
        /// <summary>
        /// 再生に用いるAudioSource
        /// </summary>
        public AudioSource AudioSource;

        /// <summary>
        /// 再生時に同時に使用するオプション
        /// </summary>
        [SerializeField] private List<OptionalVoicevoxPlayer> _optionalVoicevoxPlayers = new();

        private readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);
        private bool _isDestroyed;
        public bool IsPlaying { get; private set; }

        public IReadOnlyList<OptionalVoicevoxPlayer> OptionalVoicevoxPlayers => _optionalVoicevoxPlayers;
        
        
        private void Start()
        {
            if (AudioSource == null)
            {
                AudioSource = gameObject.GetComponent<AudioSource>();
            }

            var optionals = GetComponents<OptionalVoicevoxPlayer>();
            foreach (var opt in optionals)
            {
                if (!_optionalVoicevoxPlayers.Contains(opt))
                {
                    _optionalVoicevoxPlayers.Add(opt);
                }
            }
        }

        /// <summary>
        /// SynthesisResultを用いて音声の再生を行う
        /// OptionalVoicevoxPlayerを指定している場合はそれら全てを同時に実行し完了まで待機する
        /// </summary>
        public async UniTask PlayAsync(SynthesisResult result, CancellationToken ct)
        {
            using var linkedCts =
                CancellationTokenSource.CreateLinkedTokenSource(ct, this.GetCancellationTokenOnDestroy());
            var ct2 = linkedCts.Token;

            // 1つのVoicevoxSpeakPlayerで同時に1つの音声しか再生できないようにする
            await _semaphoreSlim.WaitAsync(ct2);

            // WavデータをAudioClipに変換
            var audioClip = AudioUtility.CreateAudioClipFromWav(result.Wav);

            try
            {
                IsPlaying = true;

                // 再生
                AudioSource.clip = audioClip;
                AudioSource.Play();

                // 再生完了まで待機
                if (_optionalVoicevoxPlayers == null || _optionalVoicevoxPlayers.Count == 0)
                {
                    await UniTask.WaitUntil(() => !AudioSource.isPlaying, cancellationToken: ct2);
                }
                else
                {
                    // オプションの再生を同時に実行
                    var audioTask = UniTask.WaitUntil(() => !AudioSource.isPlaying, cancellationToken: ct2);
                    var optionalTasks = _optionalVoicevoxPlayers.Select(player => player.PlayAsync(result, ct2))
                        .ToArray();
                    await UniTask.WhenAll(optionalTasks.Append(audioTask).ToArray());
                }
            }
            finally
            {
                if (audioClip != null) Destroy(audioClip);
                if (!_isDestroyed) _semaphoreSlim.Release(1);
                IsPlaying = false;
            }
        }

        public void AddOptionalVoicevoxPlayer(OptionalVoicevoxPlayer player)
        {
            if (!_optionalVoicevoxPlayers.Contains(player))
            {
                _optionalVoicevoxPlayers.Add(player);
            }
        }
        
        public void RemoveOptionalVoicevoxPlayer(OptionalVoicevoxPlayer player)
        {
            _optionalVoicevoxPlayers.Remove(player);
        }
        

        private void OnDestroy()
        {
            try
            {
                _isDestroyed = true;
                _semaphoreSlim.Dispose();
            }
            catch
            {
                //ignore
            }
        }
    }
}