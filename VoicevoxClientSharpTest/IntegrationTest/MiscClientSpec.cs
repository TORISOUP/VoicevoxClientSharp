using VoicevoxClientSharp.ApiClient;

namespace VoicevoxClientSharpTest.IntegrationTest;

public class MiscClientSpec : SpecBase
{
    [Test, Timeout(10000)]
    public async Task ConnectWavesAsyncTest()
    {
        // この結果を使って合成する
        var audioQuery = await QueryClient.CreateAudioQueryAsync("01", 0);
        // wavのbyte[]
        var result = await SynthesisClient.SynthesisAsync(0, audioQuery);
        // base64エンコード
        var base64 = Convert.ToBase64String(result);

        // 3つの音声をつなげる
        var waves = new[] { base64, base64, base64 };
        var connectedWaves = await MiscClient.PostConnectWavesAsync(waves, CancellationToken.None);

        Assert.IsNotNull(connectedWaves);
        Assert.Greater(connectedWaves.Length, 0);

        await PlaySoundAsync(connectedWaves);

        Assert.Pass();
    }

    [Test, Timeout(5000)]
    public async Task ValidateKanaAsyncTest()
    {
        {
            var text = "ディ'イプ/ラ'アニングワ/バンノオヤクデワアリマセ'ン";
            var (isOk, error) = await MiscClient.PostValidateKanaAsync(text, CancellationToken.None);

            Assert.IsTrue(isOk);
            Assert.IsNull(error);
        }
        {
            var text = "ディープラーニングは万能薬ではありません";
            var (isOK, error) = await MiscClient.PostValidateKanaAsync(text, CancellationToken.None);

            Assert.IsFalse(isOK);
            Assert.IsNotNull(error);
        }
    }


    [Test, Timeout(5000)]
    public async Task GetSupportedDevicesAsyncTest()
    {
        var result = await MiscClient.GetSupportedDevicesAsync(ct: CancellationToken.None);
        Assert.IsNotNull(result);
    }

}