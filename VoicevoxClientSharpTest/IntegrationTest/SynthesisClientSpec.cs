using System.IO.Compression;
using NAudio.Wave;
using VoicevoxClientSharp.ApiClient;

namespace VoicevoxClientSharpTest.IntegrationTest;

public class SynthesisClientSpec
{
    private IVoicevoxApiClient _voicevoxApiClient;
    private IQueryClient _queryClient;
    private ISynthesisClient _synthesisClient;

    [SetUp]
    public void Setup()
    {
        _voicevoxApiClient = new RawApiClient();
        _queryClient = _voicevoxApiClient;
        _synthesisClient = _voicevoxApiClient;
    }

    [TearDown]
    public void TearDown()
    {
        _voicevoxApiClient.Dispose();
    }

    private async ValueTask PlaySoundAsync(byte[] wav)
    {
        await using var stream = new MemoryStream(wav);
        await PlaySoundAsync(stream);
    }

    private async ValueTask PlaySoundAsync(Stream stream)
    {
        using var waveOut = new WaveOutEvent();
        await using var wavReader = new WaveFileReader(stream);
        waveOut.Init(wavReader);
        waveOut.Play();
        while (waveOut.PlaybackState == PlaybackState.Playing)
        {
            await Task.Delay(1000);
        }
    }


    [Test, Timeout(10000)]
    public async Task PostSynthesisAsyncTest()
    {
        // この結果を使って合成する
        var audioQuery = await _queryClient.PostAudioQueryAsync("こんにちは、世界！", 0);
        Assert.IsNotNull(audioQuery);

        var result = await _synthesisClient.PostSynthesisAsync(0, audioQuery);

        Assert.IsNotNull(result);
        Assert.Greater(result.Length, 0);

        await PlaySoundAsync(result);

        Assert.Pass();
    }

    [Test, Timeout(10000), Ignore("この API は実験的機能であり、エンジン起動時に引数で--enable_cancellable_synthesisを指定しないと有効化されません。")]
    public async Task PostCancellableSynthesisAsyncTest()
    {
        // この結果を使って合成する
        var audioQuery = await _queryClient.PostAudioQueryAsync("こんにちは、世界！", 0);
        Assert.IsNotNull(audioQuery);

        var result = await _synthesisClient.PostCancellableSynthesisAsync(0, audioQuery);
        Assert.IsNotNull(result);
        Assert.Greater(result.Length, 0);

        await PlaySoundAsync(result);
        Assert.Pass();
    }

    [Test, Timeout(10000)]
    public async Task PostMultiSpeakerSynthesisAsyncTest()
    {
        // この結果を使って合成する
        var aq1 = await _queryClient.PostAudioQueryAsync("そのいち", 0);
        var aq2 = await _queryClient.PostAudioQueryAsync("そのに", 1);
        Assert.IsNotNull(aq1);
        Assert.IsNotNull(aq2);

        var audioQueries = new[] { aq1, aq2 };

        var zip = await _synthesisClient.PostMultiSpeakerSynthesisAsync(0, audioQueries);
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
}