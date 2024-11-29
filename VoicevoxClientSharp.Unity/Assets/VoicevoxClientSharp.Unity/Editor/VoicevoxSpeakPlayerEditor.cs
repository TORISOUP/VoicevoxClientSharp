using System.Linq;
using UnityEditor;
using UnityEngine;

#if USE_VRM
using UniVRM10;
#endif

namespace VoicevoxClientSharp.Unity.Editor
{
    [CustomEditor(typeof(VoicevoxSpeakPlayer))]
    public class VoicevoxSpeakPlayerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var player = (VoicevoxSpeakPlayer)target;

            if (player.AudioSource == null)
            {
                if (GUILayout.Button("AudioSourceを自動設定"))
                {
                    var a = player.gameObject.GetComponent<AudioSource>();
                    if (a == null)
                    {
                        a = player.gameObject.AddComponent<AudioSource>();
                    }

                    player.AudioSource = a;
                }
            }

#if USE_VRM
            var vrm = player.GetComponent<Vrm10Instance>();

            if (vrm != null && player.OptionalVoicevoxPlayers.OfType<VoicevoxVrmLipSyncPlayer>().All(x => x == null))
            {
                if (GUILayout.Button("VoicevoxVrmLipSyncPlayerを追加"))
                {
                    var lipSyncPlayer = player.gameObject.GetComponent<VoicevoxVrmLipSyncPlayer>();
                    if (lipSyncPlayer == null)
                    {
                        lipSyncPlayer = player.gameObject.AddComponent<VoicevoxVrmLipSyncPlayer>();
                    }

                    lipSyncPlayer.VrmInstance = vrm;
                    player.AddOptionalVoicevoxPlayer(lipSyncPlayer);
                }
            }
#endif
        }
    }
}