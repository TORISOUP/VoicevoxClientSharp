using VoicevoxClientSharp.ApiClient.Models;

namespace VoicevoxClientSharpTest.IntegrationTest;

public class SingClientSpec : BaseSpec
{
    [Test, Timeout(15000)]
    public async Task PostSingFrameAudioQueryAsyncTest()
    {
        var score = new Score(
            new Note(key: null, frameLength: 15, lyric: "", id: null),
            new Note(key: 60, frameLength: 45, lyric: "ド", id: null),
            new Note(key: 62, frameLength: 45, lyric: "レ", id: null),
            new Note(key: null, frameLength: 15, lyric: "", id: null)
        );
        var result = await SingClient.CreateSingFrameAudioQueryAsync(score, 6000);
        Assert.IsNotNull(result);
    }


    [Test, Timeout(10000)]
    public async Task FetchSingFrameVolumeAsyncTest()
    {
        var singer = await SingClient.GetSingersAsync();
        var styleId = singer.SelectMany(x => x.Styles)
            .FirstOrDefault(x => x.Type == SpeakerType.Sing)!
            .Id;

        var score = new Score(
            new Note(key: null, frameLength: 15, lyric: "", id: null),
            new Note(key: 60, frameLength: 45, lyric: "ド", id: null),
            new Note(key: 62, frameLength: 45, lyric: "レ", id: null),
            new Note(key: null, frameLength: 15, lyric: "", id: null)
        );

        // ここで得た結果を使ってテストを続ける
        var frameAudioQuery = await SingClient.CreateSingFrameAudioQueryAsync(score, styleId);

        var result = await SingClient.FetchSingFrameVolumeAsync(styleId, score, frameAudioQuery);
        Assert.IsNotNull(result);
        Assert.That(result.Length, Is.GreaterThan(0));
    }


    [Test, Timeout(5000)]
    public async Task GetSingersAsyncTest()
    {
        var result = await SingClient.GetSingersAsync();
        Assert.IsNotNull(result);
        Assert.Greater(result.Length, 0);
        Assert.IsNotNull(result[0].Styles);
        Assert.Greater(result[0].Styles.Length, 0);
    }

    [Test, Timeout(5000)]
    public async Task GetSingerInfoAsyncTest()
    {
        var speakers = await SingClient.GetSingersAsync();
        Assert.IsNotNull(speakers);
        Assert.Greater(speakers.Length, 0);
        var speakerId = speakers[0].SpeakerUuid;

        // base64
        var resultBase64 = await SingClient.GetSingerInfoAsync(speakerId);
        Assert.IsNotNull(resultBase64);
        Assert.IsNotNull(resultBase64.Portrait);
        // httpから始まらない
        Assert.IsFalse(resultBase64.Portrait.StartsWith("http"));

        // url
        var resultUrl = await SingClient.GetSingerInfoAsync(speakerId, ResourceFormat.Url);
        Assert.IsNotNull(resultUrl);
        Assert.IsNotNull(resultUrl.Portrait);
        // httpから始まる
        Assert.IsTrue(resultUrl.Portrait.StartsWith("http"));
    }


    [Test, Timeout(10000)]
    public async Task PostFrameSynthesisAsyncTest()
    {
        var singers = await SingClient.GetSingersAsync();
        var styleId = singers.SelectMany(x => x.Styles)
            .FirstOrDefault(x => x.Type == SpeakerType.Sing)!
            .Id;

        // この結果を使って合成する
        var score = new Score(
            new Note(key: null, frameLength: 15, lyric: "", id: null),
            new Note(key: 60, frameLength: 45, lyric: "ド", id: null),
            new Note(key: 62, frameLength: 45, lyric: "レ", id: null),
            new Note(key: null, frameLength: 15, lyric: "", id: null)
        );
        var frameAudioQuery = await SingClient.CreateSingFrameAudioQueryAsync(score, styleId);

        var result = await SingClient.FrameSynthesisAsync(styleId, frameAudioQuery);

        await PlaySoundAsync(result);
        Assert.Pass();
    }
}