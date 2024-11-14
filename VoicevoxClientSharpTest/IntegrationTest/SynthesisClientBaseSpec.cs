using System.IO.Compression;
using VoicevoxClientSharp.Models;

namespace VoicevoxClientSharpTest.IntegrationTest;

public class SynthesisClientBaseSpec : BaseSpec
{
    [Test, Timeout(10000)]
    public async Task PostSynthesisAsyncTest()
    {
        // この結果を使って合成する
        var audioQuery = await QueryClient.CreateAudioQueryAsync("こんにちは、世界！", 0);
        Assert.IsNotNull(audioQuery);

        var result = await SynthesisClient.SynthesisAsync(0, audioQuery);

        Assert.IsNotNull(result);
        Assert.Greater(result.Length, 0);

        await PlaySoundAsync(result);

        Assert.Pass();
    }

    [Test, Timeout(10000), Ignore("この API は実験的機能であり、エンジン起動時に引数で--enable_cancellable_synthesisを指定しないと有効化されません。")]
    public async Task PostCancellableSynthesisAsyncTest()
    {
        // この結果を使って合成する
        var audioQuery = await QueryClient.CreateAudioQueryAsync("こんにちは、世界！", 0);
        Assert.IsNotNull(audioQuery);

        var result = await SynthesisClient.CancellableSynthesisAsync(0, audioQuery);
        Assert.IsNotNull(result);
        Assert.Greater(result.Length, 0);

        await PlaySoundAsync(result);
        Assert.Pass();
    }

    [Test, Timeout(10000)]
    public async Task PostMultiSpeakerSynthesisAsyncTest()
    {
        // この結果を使って合成する
        var aq1 = await QueryClient.CreateAudioQueryAsync("そのいち", 0);
        var aq2 = await QueryClient.CreateAudioQueryAsync("そのに", 1);
        Assert.IsNotNull(aq1);
        Assert.IsNotNull(aq2);

        var audioQueries = new[] { aq1, aq2 };

        var zip = await SynthesisClient.MultiSpeakerSynthesisAsync(0, audioQueries);
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
    public async Task PostFrameSynthesisAsyncTest()
    {
        // この結果を使って合成する
        var score = new Score(
            new Note(key: null, frameLength: 15, lyric: "", id: null),
            new Note(key: 60, frameLength: 45, lyric: "ド", id: null),
            new Note(key: 62, frameLength: 45, lyric: "レ", id: null),
            new Note(key: null, frameLength: 15, lyric: "", id: null)
        );
        var frameAudioQuery = await QueryClient.CreateSingFrameAudioQueryAsync(score, 6000);

        var result = await SynthesisClient.FrameSynthesisAsync(6000, frameAudioQuery);

        await PlaySoundAsync(result);
        Assert.Pass();
    }

    [Test, Timeout(10000)]
    public async Task PostMorphableTargetsAsyncTest()
    {
        var result =
            await SynthesisClient.IsMorphableTargetsAsync([
                0, 1, 2
            ]);

        Assert.IsNotNull(result);
        Assert.That(result.Length, Is.EqualTo(3));
        Assert.Greater(result[0].Count, 0);
        Assert.Greater(result[1].Count, 0);
        Assert.Greater(result[2].Count, 0);
    }

    [Test, Timeout(10000)]
    public async Task PostSynthesisMorphingAsyncTest()
    {
        // この結果を使って合成する
        var audioQuery = await QueryClient.CreateAudioQueryAsync("こんにちは、世界！", 0);
        Assert.IsNotNull(audioQuery);

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