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

// --------------------------------------------

// 初期化
using var synthesizer = new VoicevoxSynthesizer();

// スタイルIDを取得
var styleId = (await synthesizer.FindStyleIdByNameAsync("ずんだもん", "あまあま"))!.Value;


// そのまま喋らせる
{
    var wav = await synthesizer.SpeakAsync(styleId, "こんにちは、世界！");
    await PlaySoundAsync(wav);
}

// パラメータを上書きしてみる
{
    var wav = await synthesizer.SpeakAsync(styleId, "こんにちは、世界！",
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
    var wav = await synthesizer.SpeakWithPresetAsync(1, "こんにちは、世界！");
    await PlaySoundAsync(wav);
}


try
{
    // 存在しないプリセットを使ってみる
    var wav = await synthesizer.SpeakWithPresetAsync(0, "こんにちは、世界！");
    await PlaySoundAsync(wav);
}
catch (VoicevoxClientException ex)
{
    // 失敗するはず
    var wav = await synthesizer.SpeakAsync(0, ex.Message);
    await PlaySoundAsync(wav);
}


{
    // 合成可能？
    var isMorphable = await synthesizer.CanMorphAsync(0, 2);
    var wav = await synthesizer.SpeakAsync(styleId,
        "0と2は" + (isMorphable ? "モーフィング可能です" : "モーフィング不可能です"));
    await PlaySoundAsync(wav);
}

{
    // 合成可能？
    var isMorphable = await synthesizer.CanMorphAsync(0, 1);
    var wav = await synthesizer.SpeakAsync(styleId,
        "0と1は" + (isMorphable ? "モーフィング可能です" : "モーフィング不可能です"));
    await PlaySoundAsync(wav);
}


{
    // モーフィングしてみる
    var wav = await synthesizer.SpeakMorphingAsync(0, 2, 0.5M, "こんにちは、世界！");
    await PlaySoundAsync(wav);
}

