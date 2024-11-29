using NAudio.Wave;
using VoicevoxClientSharp;
using VoicevoxClientSharp.ApiClient;

static async ValueTask PlaySoundAsync(byte[] wav)
{
    await using var stream = new MemoryStream(wav);
    using var waveOut = new WaveOutEvent();
    await using var wavReader = new WaveFileReader(stream);
    waveOut.Init(wavReader);
    waveOut.Play();
    while (waveOut.PlaybackState == PlaybackState.Playing)
    {
        await Task.Delay(1000);
    }
}

{
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
}

// --------------------------------------------
{
    // コンストラクタ引数未指定の場合はデフォルト設定で初期化します
    // デフォルト設定
    //  - 接続先: http://localhost:50021
    //  - 内部的に生成したHttpClientを用いる
    using var synthesizer1 = new VoicevoxSynthesizer();

    // 接続先を指定したい場合はVoicevoxApiClientを手動で生成し、
    // それをコンストラクタ引数に渡してください
    var apiClient = VoicevoxApiClient.Create(baseUri: "http://localhost:50021");
    using var synthesizer2 = new VoicevoxSynthesizer(apiClient);
}
{
// VoicevoxSynthesizerの初期化
    using var synthesizer = new VoicevoxSynthesizer();

// スタイルIDを取得
// StyleIdが既知の場合はこのステップは不要
    var styleId = (await synthesizer.FindStyleIdByNameAsync(speakerName: "ずんだもん", styleName: "あまあま"))!.Value;

// スタイルを初期化
// 省略可能だが、省略すると初回の合成時に時間がかかる
    await synthesizer.InitializeStyleAsync(styleId);


// そのまま喋らせる
    {
        var (wav, _, _) = await synthesizer.SynthesizeSpeechAsync(styleId, "こんにちは、世界！");
        await PlaySoundAsync(wav);
    }

// パラメータを上書きしてみる
    {
        var (wav, _, _) = await synthesizer.SynthesizeSpeechAsync(styleId, "こんにちは、世界！",
            speedScale: 1.1M,
            pitchScale: 0.1M,
            intonationScale: 1.1M,
            volumeScale: 0.5M,
            prePhonemeLength: 0.1M,
            postPhonemeLength: 0.1M,
            pauseLength: 0.1M,
            pauseLengthScale: 1.5M);
        await PlaySoundAsync(wav);
    }

// プリセットを使って喋らせる
    {
        var (wav, _, _) = await synthesizer.SynthesizeSpeechWithPresetAsync(presetId: 1, "こんにちは、世界！");
        await PlaySoundAsync(wav);
    }


    try
    {
        // 存在しないプリセットを使ってみる
        var (wav, _, _) = await synthesizer.SynthesizeSpeechWithPresetAsync(0, "こんにちは、世界！");
        await PlaySoundAsync(wav);
    }
    catch (VoicevoxClientException ex)
    {
        // 失敗するはず
        var (wav, _, _) = await synthesizer.SynthesizeSpeechAsync(0, ex.Message);
        await PlaySoundAsync(wav);
    }


    {
        // 合成可能？
        var isMorphable = await synthesizer.CanMorphAsync(baseStyleId: 0, targetStyleId: 2);
        var (wav, _, _) = await synthesizer.SynthesizeSpeechAsync(styleId,
            "0と2は" + (isMorphable ? "モーフィング可能です" : "モーフィング不可能です"));
        await PlaySoundAsync(wav);
    }

    {
        // 合成可能？
        var isMorphable = await synthesizer.CanMorphAsync(0, 1);
        var (wav, _, _) = await synthesizer.SynthesizeSpeechAsync(styleId,
            "0と1は" + (isMorphable ? "モーフィング可能です" : "モーフィング不可能です"));
        await PlaySoundAsync(wav);
    }


    {
        // モーフィングしてみる
        var (wav, _, _) = await synthesizer.SpeakMorphingAsync(0, 2, 0.5M, "こんにちは、世界！");
        await PlaySoundAsync(wav);
    }
}