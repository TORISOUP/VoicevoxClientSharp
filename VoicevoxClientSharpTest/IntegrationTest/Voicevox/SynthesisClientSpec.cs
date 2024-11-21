using System.IO.Compression;

namespace VoicevoxClientSharpTest.IntegrationTest.Voicevox;

public class SynthesisClientSpec : BaseSpec
{
    [Test, Timeout(10000)]
    public async Task PostSynthesisAsyncTest()
    {
        var styleId = await GetDefaultStyleIdAsync();

        // この結果を使って合成する
        var audioQuery = await QueryClient.CreateAudioQueryAsync("こんにちは、世界！", styleId);
        Assert.IsNotNull(audioQuery);

        var result = await SynthesisClient.SynthesisAsync(styleId, audioQuery);

        Assert.IsNotNull(result);
        Assert.Greater(result.Length, 0);

        await PlaySoundAsync(result);

        Assert.Pass();
    }

    [Test, Timeout(10000), Ignore("この API は実験的機能であり、エンジン起動時に引数で--enable_cancellable_synthesisを指定しないと有効化されません。")]
    public async Task PostCancellableSynthesisAsyncTest()
    {
        var styleId = await GetDefaultStyleIdAsync();

        // この結果を使って合成する
        var audioQuery = await QueryClient.CreateAudioQueryAsync("こんにちは、世界！", styleId);
        Assert.IsNotNull(audioQuery);

        var result = await SynthesisClient.CancellableSynthesisAsync(styleId, audioQuery);
        Assert.IsNotNull(result);
        Assert.Greater(result.Length, 0);

        await PlaySoundAsync(result);
        Assert.Pass();
    }

    [Test, Timeout(10000)]
    public async Task PostMultiSpeakerSynthesisAsyncTest()
    {
        var styleId = await GetDefaultStyleIdAsync();

        // この結果を使って合成する
        var aq1 = await QueryClient.CreateAudioQueryAsync("そのいち", styleId);
        var aq2 = await QueryClient.CreateAudioQueryAsync("そのに", styleId + 1);
        Assert.IsNotNull(aq1);
        Assert.IsNotNull(aq2);

        var audioQueries = new[] { aq1, aq2 };

        var zip = await SynthesisClient.MultiSpeakerSynthesisAsync(styleId, audioQueries);
        Assert.IsNotNull(zip);
        Assert.Greater(zip.Length, 0);

        // zipを解凍して再生する
        using var zipStream = new MemoryStream(zip);
        using var zipArchive = new ZipArchive(zipStream);
        foreach (var entry in zipArchive.Entries)
        {
            if (entry.FullName.EndsWith(".wav", StringComparison.OrdinalIgnoreCase))
            {
                using var wavStream = new MemoryStream();
                await using var entryStream = entry.Open();
                await entryStream.CopyToAsync(wavStream);
                wavStream.Position = 0;
                await PlaySoundAsync(wavStream);
            }
        }
    }

    [Test, Timeout(10000)]
    public async Task PostMorphableTargetsAsyncTest()
    {
        var styleId = await GetDefaultStyleIdAsync();
        
        var result = await SynthesisClient.IsMorphableTargetsAsync([styleId]);

        Assert.IsNotNull(result);
        Assert.That(result.Length, Is.EqualTo(1));
        Assert.Greater(result[0].Count, 0);
    }

    [Test, Timeout(10000)]
    public async Task PostSynthesisMorphingAsyncTest()
    {
        var styleId = await GetDefaultStyleIdAsync();
        
        // この結果を使って合成する
        var audioQuery = await QueryClient.CreateAudioQueryAsync("こんにちは、世界！", styleId);
        Assert.IsNotNull(audioQuery);

        var dict = await SynthesisClient.IsMorphableTargetsAsync([styleId]);

        var key = styleId.ToString();
        var morphTarget = dict[0].FirstOrDefault(x => x.Key != key && x.Value.IsMorphable);
        
        if(morphTarget.Value == null)
        {
            Assert.Ignore("morphTarget is null");
        }
        
        var result = await SynthesisClient.SynthesisMorphingAsync(
            2, // 四国めたん ノーマル
            37, // 四国めたん ヒソヒソ
            0.5m, audioQuery);

        Assert.IsNotNull(result);
        Assert.Greater(result.Length, 0);
        await PlaySoundAsync(result);
        Assert.Pass();
    }
}