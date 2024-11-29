using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace VoicevoxClientSharp.Unity
{
    /// <summary>
    /// VoicevoxSpeakPlayerと連携するためのクラス
    /// </summary>
    public abstract class OptionalVoicevoxPlayer : MonoBehaviour
    {
        /// <summary>
        /// 再生中かどうか
        /// </summary>
        public bool IsPlaying { get; protected set; }
        
        /// <summary>
        /// 再生を開始する
        /// </summary>
        public abstract UniTask PlayAsync(SynthesisResult synthesisResult, CancellationToken cancellationToken);
    }
}