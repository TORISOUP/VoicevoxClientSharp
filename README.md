# VoicevoxClientSharp

[VOIVEVOX.exe](https://voicevox.hiroshiba.jp/)および[voicevox_engine](https://github.com/VOICEVOX/voicevox_engine)および[AvisSpeech](https://aivis-project.com/)をC#から制御するためのクライアントライブラリです。 
.NET Standard 2.0向けに作成されています。


* 対象プラットフォーム: `.NET Standard 2.0`
  * .NET Core 2.0以降
  * .NET Framework 4.6.1以降
  * Unity 2018.4以降
  * etc.

* 対象のVOICEVOXエンジンバージョン: `0.21.1`
* 対象のAvisSpeechバージョン: `1.0.0`


またUnity用のプラグインも提供しています。さらに、[VRM](https://vrm.dev/)と連携して使用することもできます。

* 対象のVRMバージョン: `v0.128.0`


## インストール方法

### .NET環境向けのインストール方法

NuGetパッケージマネージャーからインストールすることができます。

```
Install-Package VoicevoxClientSharp
```

### Unity向けのインストール方法

1. NuGetより`VoicevoxClientSharp`をUnityプロジェクトに導入


2. `VoicevoxClientSharp.Unity`をUPMより参照してインストール


## 使い方

テキストより音声合成を行うことが目的の場合は`VoicevoxSynthesizer`を使用するのが便利です。  
一方で、VOICEVOXが提供するREST APIを個別に実行したい場合は`VoicevoxApiClient`を使用してください。

### VoicevoxSynthesizer : 音声合成の簡易に行うためのクラス

`VoicevoxSynthesizer`を用いることでテキストから音声合成を簡単に実行できます。
また音声合成に必要な下準備も簡易ですが行うことができます。

#### 基本的な使い方

※ **VoicevoxSynthesizerはAvisSpeechには対応していません。VOICEVOX専用です。**

```cs
// VoicevoxSynthesizerの初期化
using var synthesizer = new VoicevoxSynthesizer();

// スタイルIDを取得
// StyleIdが既知の場合はこのステップは不要
int styleId = (await synthesizer.FindStyleIdByNameAsync(speakerName: "ずんだもん", styleName: "あまあま"))!.Value;

// スタイルを初期化
// 省略可能だが、省略すると初回の合成時に時間がかかる
await synthesizer.InitializeStyleAsync(styleId);

// 音声合成を実行
// resultに合成結果のwavデータ(byte[])が可能されている
SynthesisResult result = await synthesizer.SynthesizeSpeechAsync(styleId, "こんにちは、世界！");
```

`SynthesisResult`には発話に使用したクエリ（`AudioQuery`）と合成結果のwav（`byte[]`）が含まれています。


```cs
/// <summary>
/// VOICEVOXの発話音声の合成結果
/// </summary>
public readonly struct SynthesisResult : IEquatable<SynthesisResult>
{
    /// <summary>
    /// 合成した音声データ
    /// </summary>
    public byte[] Wav { get; }

    /// <summary>
    /// 音声合成に使用したクエリ
    /// </summary>
    public AudioQuery AudioQuery { get; }
}
```

#### 音声合成のパラメータ指定

```csharp
// 個別のパラメータを指定した音声合成も可能
var result = await synthesizer.SynthesizeSpeechAsync(styleId, "こんにちは、世界！",
        speedScale: 1.1M,
        pitchScale: 0.1M,
        intonationScale: 1.1M,
        volumeScale: 0.5M,
        prePhonemeLength: 0.1M,
        postPhonemeLength: 0.1M,
        pauseLength: 0.1M,
        pauseLengthScale: 1.5M);

// VOICEVOXに登録されたプリセットを用いて音声合成も可能
result = await synthesizer.SynthesizeSpeechWithPresetAsync(presetId: 1, "こんにちは、世界！");
```

#### モーフィング

```cs
// 2つのスタイルが合成可能であるか
var isMorphable = await synthesizer.CanMorphAsync(baseStyleId: 0, targetStyleId: 2);
if (isMorphable)
{
    // 合成可能なら半々の割合でモーフィング
    var result = await synthesizer.SpeakMorphingAsync(0, 2, 0.5M, "こんにちは、世界！");
}
```

#### 接続先の変更

コンストラクタ引数を何も指定しない場合は`http://localhost:50021`を接続先として初期化します。
接続先を変更したい場合は`VoicevoxApiClient`を手動で生成し、`VoicevoxSynthesizer`のコンストラクタに渡してください。

```cs
// 接続先を指定したい場合はVoicevoxApiClientを手動で生成し、
// それをコンストラクタ引数に渡してください
var apiClient = VoicevoxApiClient.Create(baseUri: "http://localhost:50021");
using var synthesizer = new VoicevoxSynthesizer(apiClient);
```

### VoicevoxApiClient : REST APIを実行するシンプルなクライアント実装

`VoicevoxApiClient`はVOICEVOX（およびAvisSpeech）が提供するREST APIと1:1に対応した"シンプル"なクライアントです。


#### 使い方

[VOICEVOXのAPIドキュメント](https://voicevox.github.io/voicevox_engine/api/)を参考に、実行したいAPIに対応したメソッドを呼び出してください。


| REST API Endpoint                            | HTTP Method | メソッド名                                    | 説明                                           |
|----------------------------------------------|-------------|-----------------------------------------------|----------------------------------------------|
| `/downloadable_libraries`                    | GET         | `GetDownloadableLibrariesAsync`              | ライブラリ情報一覧を取得する                   |
| `/installed_libraries`                       | GET         | `GetInstalledLibrariesAsync`                 | インストール済み音声ライブラリ情報を取得する    |
| `/install_library/{library_uuid}`            | POST        | `InstallLibraryAsync`                        | 音声ライブラリをインストールする               |
| `/uninstall_library/{library_uuid}`          | POST        | `UninstallLibraryAsync`                      | 音声ライブラリをアンインストールする           |
| `/connect_waves`                             | POST        | `PostConnectWavesAsync`                      | 複数のWAVデータを1つに統合する                 |
| `/validate_kana`                             | POST        | `PostValidateKanaAsync`                      | AquesTalk風記法のバリデーション                 |
| `/supported_devices`                         | GET         | `GetSupportedDevicesAsync`                   | 対応デバイス一覧を取得する                     |
| `/version`                                   | GET         | `GetVersionAsync`                            | エンジンのバージョンを取得する                 |
| `/core_versions`                             | GET         | `GetCoreVersionsAsync`                       | 利用可能なコアのバージョン一覧を取得する       |
| `/engine_manifest`                           | GET         | `GetEngineManifestAsync`                     | エンジンのマニフェストを取得する               |
| `/presets`                                   | GET         | `GetPresetsAsync`                            | プリセット設定を取得する                        |
| `/add_preset`                                | POST        | `AddPresetAsync`                             | 新しいプリセットを追加する                     |
| `/update_preset`                             | POST        | `UpdatePresetAsync`                          | プリセットを更新する                            |
| `/delete_preset`                             | POST        | `DeletePresetAsync`                          | プリセットを削除する                            |
| `/audio_query`                               | POST        | `CreateAudioQueryAsync`                      | 音声合成用クエリを作成する                     |
| `/audio_query_from_preset`                   | POST        | `CreateAudioQueryFromPresetAsync`            | プリセットを用いて音声合成用クエリを作成する    |
| `/accent_phrases`                            | POST        | `CreateAccentPhraseAsync`                    | アクセント句を取得する                          |
| `/mora_data`                                 | POST        | `FetchMoraDataAsync`                         | アクセント句から音高・音素長を取得する           |
| `/mora_length`                               | POST        | `FetchMoraLengthAsync`                       | アクセント句から音素長を取得する                |
| `/mora_pitch`                                | POST        | `FetchMoraPitchAsync`                        | アクセント句から音高を取得する                  |
| `/sing_frame_audio_query`                    | POST        | `CreateSingFrameAudioQueryAsync`             | 歌唱音声合成用クエリを作成する                 |
| `/sing_frame_volume`                         | POST        | `FetchSingFrameVolumeAsync`                  | フレームごとの音量を取得する                    |
| `/frame_synthesis`                           | POST        | `FrameSynthesisAsync`                        | 歌唱音声合成を実行する                          |
| `/singers`                                   | GET         | `GetSingersAsync`                            | 歌唱可能なキャラクター一覧を取得する            |
| `/singer_info`                               | GET         | `GetSingerInfoAsync`                         | キャラクターの詳細情報を取得する                |
| `/initialize_speaker`                        | POST        | `InitializeSpeakerAsync`                     | スピーカーのスタイルを初期化する                |
| `/is_initialized_speaker`                    | GET         | `IsInitializedSpeakerAsync`                  | スピーカーが初期化されているか確認する          |
| `/speakers`                                  | GET         | `GetSpeakersAsync`                           | 喋れるキャラクター一覧を取得する                |
| `/speaker_info`                              | GET         | `GetSpeakerInfoAsync`                        | キャラクターの詳細情報を取得する                |
| `/synthesis`                                 | POST        | `SynthesisAsync`                             | 音声合成を行う                                  |
| `/cancellable_synthesis`                     | POST        | `CancellableSynthesisAsync`                  | キャンセル可能な音声合成を行う                  |
| `/multi_synthesis`                           | POST        | `MultiSpeakerSynthesisAsync`                 | 複数の音声合成をまとめて行う                   |
| `/user_dict`                                 | GET         | `GetUserDictionaryWordsAsync`                | ユーザー辞書の単語一覧を取得する                |
| `/user_dict_word`                            | POST        | `AddUserDictionaryWordAsync`                 | ユーザー辞書に単語を追加する                    |
| `/user_dict_word/{word_uuid}`                | POST        | `UpdateUserDictionaryWordAsync`              | ユーザー辞書の単語を更新する                    |
| `/user_dict_word/{word_uuid}`                | DELETE      | `DeleteUserDictionaryWordAsync`              | ユーザー辞書の単語を削除する                    |
| `/import_user_dict`                          | POST        | `ImportUserDictionaryWordsAsync`             | 他のユーザー辞書をインポートする                |



```cs
// APIクライアントを生成
using var apiClient = VoicevoxApiClient.Create();

// GET /speakers
// スピーカー一覧を取得
var speakers = await apiClient.GetSpeakersAsync();

// スピーカー名とスタイル名からスタイルIDを取得
var speaker = speakers.FirstOrDefault(s => s.Name == "ずんだもん");
var styleId = speaker?.Styles.FirstOrDefault(x => x.Name == "あまあま")!.Id ?? 0;

// POST /audio_query
// 音声合成用のクエリを作成
var audioQuery = await apiClient.CreateAudioQueryAsync("こんにちは、世界！", styleId);

// POST /synthesis
// 音声合成を実行
byte[] wav = await apiClient.SynthesisAsync(styleId, audioQuery);
```





#### 初期化について

VOICEVOXに接続する場合は`VoicevoxApiClient`をそのまま利用できます。**AvisSpeechに接続する場合は`IAvisSpeechApiClient`にキャストするか、専用のファクトリーメソッドを使用してください。**

```cs
// VOICEVOXに接続する場合はコンストラクタを利用してもよいし
using var voicevoxApiClient1 = new VoicevoxApiClient();
// VoicevoxApiClient.Createを使用してもよい
using var voicevoxApiClient2 = VoicevoxApiClient.Create();

// AviSpeechに接続する場合はIAvisSpeechApiClientに明示的にキャストするか
using var avisSpeechApiClient1 = new VoicevoxApiClient() as IAvisSpeechApiClient;
// VoicevoxApiClient.CreateForAvisSpeechを使用してください
using var avisSpeechApiClient2 = VoicevoxApiClient.CreateForAvisSpeech();
```

また、引数として接続先情報と`HttpClient`を指定できます。接続方法をカスタムしたい場合に利用してください。
なお引数で`HttpClient`を指定した場合、その`HttpClient`の`Dispose()`は`VoicevoxApiClient`側で呼び出すことはありません。

```cs
// デフォルトの接続先で初期化 : http://localhost:50021
using var apiClient = VoicevoxApiClient.Create();


// 接続先を指定して初期化
using var apiClient2 = VoicevoxApiClient.Create(baseUri: "http://localhost:50021");


// HttpClientを指定
var httpClient = new HttpClient()
{
    // タイムアウトを5秒に設定
    Timeout = TimeSpan.FromSeconds(5)
};
// HttpClientを指定して生成
using var apiClient3 = VoicevoxApiClient.Create(httpClient);


// HttpClientと接続先を指定
using var apiClient4 = VoicevoxApiClient.Create(baseUri: "http://localhost:50021", httpClient);
```