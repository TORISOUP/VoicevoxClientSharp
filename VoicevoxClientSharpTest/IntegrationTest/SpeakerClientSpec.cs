using VoicevoxClientSharp.ApiClient;
using VoicevoxClientSharp.ApiClient.Models;

namespace VoicevoxClientSharpTest.IntegrationTest;

public class SpeakerClientSpeck : BaseSpec
{
    [Test, Timeout(5000)]
    public async Task InitializeSpeakerAsyncTest()
    {
        var styleId = await GetDefaultStyleIdAsync();
        Assert.DoesNotThrowAsync(async () => await SpeakerClient.InitializeSpeakerAsync(styleId));

        Assert.ThrowsAsync<VoicevoxApiErrorException>(async () => { await SpeakerClient.InitializeSpeakerAsync(-1); });
    }

    [Test, Timeout(5000)]
    public void IsInitializedAsyncTest()
    {
        Assert.DoesNotThrowAsync(async () =>
        {
            var styleId = await GetDefaultStyleIdAsync();
            await SpeakerClient.InitializeSpeakerAsync(styleId);
            var result = await SpeakerClient.IsInitializedSpeakerAsync(styleId);
            Assert.IsTrue(result);
        });
    }

    [Test, Timeout(5000)]
    public async Task GetSpeakersAsyncTest()
    {
        var result = await SpeakerClient.GetSpeakersAsync();
        Assert.IsNotNull(result);
        Assert.Greater(result.Length, 0);
        Assert.IsNotNull(result[0].Styles);
        Assert.Greater(result[0].Styles.Length, 0);
    }

    [Test, Timeout(5000)]
    public async Task GetSpeakerInfoAsyncTest()
    {
        var speakers = await SpeakerClient.GetSpeakersAsync();
        Assert.IsNotNull(speakers);
        Assert.Greater(speakers.Length, 0);
        var speakerId = speakers[0].SpeakerUuid;

        // base64
        var resultBase64 = await SpeakerClient.GetSpeakerInfoAsync(speakerId);
        Assert.IsNotNull(resultBase64);
        Assert.IsNotNull(resultBase64.Portrait);
        // httpから始まらない
        Assert.IsFalse(resultBase64.Portrait.StartsWith("http"));

        // url
        var resultUrl = await SpeakerClient.GetSpeakerInfoAsync(speakerId, ResourceFormat.Url);
        Assert.IsNotNull(resultUrl);
        Assert.IsNotNull(resultUrl.Portrait);
        // httpから始まる
        Assert.IsTrue(resultUrl.Portrait.StartsWith("http"));
    }

    [Test, Timeout(5000)]
    public async Task GetSingersAsyncTest()
    {
        var result = await SpeakerClient.GetSingersAsync();
        Assert.IsNotNull(result);
        Assert.Greater(result.Length, 0);
        Assert.IsNotNull(result[0].Styles);
        Assert.Greater(result[0].Styles.Length, 0);
    }

    [Test, Timeout(5000)]
    public async Task GetSingerInfoAsyncTest()
    {
        var speakers = await SpeakerClient.GetSingersAsync();
        Assert.IsNotNull(speakers);
        Assert.Greater(speakers.Length, 0);
        var speakerId = speakers[0].SpeakerUuid;

        // base64
        var resultBase64 = await SpeakerClient.GetSingerInfoAsync(speakerId);
        Assert.IsNotNull(resultBase64);
        Assert.IsNotNull(resultBase64.Portrait);
        // httpから始まらない
        Assert.IsFalse(resultBase64.Portrait.StartsWith("http"));

        // url
        var resultUrl = await SpeakerClient.GetSingerInfoAsync(speakerId, ResourceFormat.Url);
        Assert.IsNotNull(resultUrl);
        Assert.IsNotNull(resultUrl.Portrait);
        // httpから始まる
        Assert.IsTrue(resultUrl.Portrait.StartsWith("http"));
    }
}