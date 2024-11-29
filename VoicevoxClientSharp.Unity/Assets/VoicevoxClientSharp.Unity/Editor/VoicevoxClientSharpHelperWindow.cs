using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using VoicevoxClientSharp.ApiClient;
using VoicevoxClientSharp.ApiClient.Models;

namespace VoicevoxClientSharp.Unity.Editor
{
    public class VoicevoxClientSharpHelperWindow : EditorWindow
    {
        [MenuItem("VoicevoxClientSharp/Open HelperWindow")]
        private static void Open()
        {
            var window = GetWindow<VoicevoxClientSharpHelperWindow>();
            window.name = "VoicevoxClientSharpHelper";
            window.titleContent = new GUIContent("VoicevoxClientSharpHelper");
        }

        private string _inputUrl = "http://localhost:50021";
        private VoicevoxApiClient _voicevoxApiClient;
        private bool _isValidUrl = false;
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();
        private readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);
        private Speaker[] _speakers;
        private string _voicevoxVersion;

        private void OnGUI()
        {
            using (new GUILayout.VerticalScope())
            {
                using (new GUILayout.HorizontalScope(GUI.skin.box))
                {
                    GUILayout.Label("URL");
                    _inputUrl = GUILayout.TextField(_inputUrl);

                    // セマフォが無効な場合はDisableにする
                    using (new EditorGUI.DisabledScope(_semaphoreSlim.CurrentCount == 0))
                    {
                        if (GUILayout.Button("Connect"))
                        {
                            _voicevoxApiClient?.Dispose();
                            _voicevoxApiClient = new VoicevoxApiClient(_inputUrl);
                            ConnectionTestAsync(_cts.Token).Forget();
                        }
                    }
                }

                if (_isValidUrl)
                {
                    VoicevoxTestGui();
                }
            }
        }

        private void VoicevoxTestGui()
        {
            GUILayout.Label($"VOICEVOX version: {_voicevoxVersion}");

            using (new GUILayout.HorizontalScope(GUI.skin.box))
            {
                using (new EditorGUI.DisabledScope(_semaphoreSlim.CurrentCount == 0))
                {
                    if (GUILayout.Button("Get Speaker List"))
                    {
                        // SpeakerListを取得
                        GetSpeakersAsync(_cts.Token).Forget();
                    }
                }
            }

            using (new GUILayout.VerticalScope(GUI.skin.box))
            {
                DrawSpeakerList();
            }
        }

        private Vector2 _speakerListScrollPos;

        private void DrawSpeakerList()
        {
            if (_speakers == null) return;
            using (var scope = new GUILayout.ScrollViewScope(_speakerListScrollPos))
            {
                _speakerListScrollPos = scope.scrollPosition;
                foreach (var speaker in _speakers)
                {
                    // スクロールビューにする

                    using (new GUILayout.VerticalScope(GUI.skin.box))
                    {
                        using (new GUILayout.HorizontalScope())
                        {
                            var speakerId = speaker.SpeakerUuid;
                            GUILayout.Label("Speaker: " + speaker.Name);
                            GUILayout.TextField(speakerId);
                            GUILayout.FlexibleSpace();
                        }


                        foreach (var style in speaker.Styles)
                        {
                            using (new GUILayout.HorizontalScope())
                            {
                                var styleId = style.Id;
                                GUILayout.Label("Style: " + style.Name);
                                GUILayout.FlexibleSpace();

                                GUILayout.Label("StyleId:");
                                GUILayout.TextField(styleId.ToString(), GUILayout.Width(100));
                                GUILayout.FlexibleSpace();
                            }
                        }
                    }
                }
            }
        }


        private async UniTask ConnectionTestAsync(CancellationToken ct)
        {
            try
            {
                await _semaphoreSlim.WaitAsync(ct);
                _voicevoxVersion = await _voicevoxApiClient.GetVersionAsync(ct);
                _isValidUrl = true;
            }
            catch (Exception e) when (!(e is OperationCanceledException))
            {
                Debug.LogError(e);
                _isValidUrl = false;
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        private async UniTask GetSpeakersAsync(CancellationToken ct)
        {
            try
            {
                await _semaphoreSlim.WaitAsync(ct);
                _speakers = await _voicevoxApiClient.GetSpeakersAsync(cancellationToken: ct);
            }
            catch (Exception e) when (!(e is OperationCanceledException))
            {
                Debug.LogError(e);
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        private void OnDestroy()
        {
            _cts.Cancel();
            _cts.Dispose();
            _voicevoxApiClient?.Dispose();
            _semaphoreSlim.Dispose();
        }
    }
}