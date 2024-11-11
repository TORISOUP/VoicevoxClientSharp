using VoicevoxClientSharp.ApiClient;

namespace VoicevoxClientSharpTest.IntegrationTest;

public class SpeakerClientSpeck : SpecBase
{
    [Test, Timeout(5000)]
    public void InitializeSpeakerAsyncTest()
    {
        Assert.DoesNotThrowAsync(async () => await SpeakerClient.InitializeSpeakerAsync(0));

        Assert.ThrowsAsync<VoicevoxApiErrorException>(async () => { await SpeakerClient.InitializeSpeakerAsync(-1); });
    }

    [Test, Timeout(5000)]
    public void IsInitializedAsyncTest()
    {
        Assert.DoesNotThrowAsync(async () =>
        {
            await SpeakerClient.InitializeSpeakerAsync(0);
            var result = await SpeakerClient.IsInitializedSpeakerAsync(0);
            Assert.IsTrue(result);
        });
    }

    [Test, Timeout(5000)]
    public void GetSpeakersAsyncTest()
    {
        Assert.DoesNotThrowAsync(async () =>
        {
            var result = await SpeakerClient.GetSpeakersAsync();
            Assert.IsNotNull(result);
            Assert.Greater(result.Length, 0);
            Assert.IsNotNull(result[0].Styles);
            Assert.Greater(result[0].Styles.Length, 0);
        });
    }
}