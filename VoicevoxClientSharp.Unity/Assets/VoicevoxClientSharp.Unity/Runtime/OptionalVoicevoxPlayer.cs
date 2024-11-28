using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace VoicevoxClientSharp.Unity
{
    public abstract class OptionalVoicevoxPlayer : MonoBehaviour
    {
        public bool IsPlaying { get; protected set; }
        public abstract UniTask PlayAsync(SynthesisResult synthesisResult, CancellationToken cancellationToken);
    }
}