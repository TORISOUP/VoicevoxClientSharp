namespace VoicevoxClientSharpTest.IntegrationTest.Voicevox;

public class MiscClientBaseSpec : BaseSpec
{
    [Test, Timeout(10000)]
    public async Task ConnectWavesAsyncTest()
    {
        var styleId = await GetDefaultStyleIdAsync();
        
        // この結果を使って合成する
        var audioQuery = await QueryClient.CreateAudioQueryAsync("01", styleId);
        // wavのbyte[]
        var result = await SynthesisClient.SynthesisAsync(styleId, audioQuery);
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
    public async Task GetVersionAsyncTest()
    {
        var result = await MiscClient.GetVersionAsync(cancellationToken: CancellationToken.None);
        Assert.IsNotNull(result);
    }

    [Test, Timeout(5000)]
    public async Task GetCoreVersionsAsyncTest()
    {
        var result = await MiscClient.GetCoreVersionsAsync(cancellationToken: CancellationToken.None);
        Assert.IsNotNull(result);
        Assert.Greater(result.Length, 0);
    }
    
    [Test, Timeout(5000)]
    public async Task GetEngineManifestAsyncTest()
    {
        var result = await MiscClient.GetEngineManifestAsync(cancellationToken: CancellationToken.None);
        Assert.IsNotNull(result);
        Assert.IsNotNull(result.SupportedFeatures);
        Assert.Greater(result.DependencyLicenses.Length, 0);
        Assert.Greater(result.UpdateInfos.Length, 0);
    }
}