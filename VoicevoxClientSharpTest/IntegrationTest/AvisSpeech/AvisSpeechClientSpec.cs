using System.IO.Compression;
using NAudio.Wave;
using VoicevoxClientSharp.ApiClient;

namespace VoicevoxClientSharpTest.IntegrationTest.AvisSpeech;

public class AvisSpeechClientSpec
{
    #region Setup

    private IAvisSpeechApiClient _client;

    [SetUp]
    public void Setup()
    {
        _client = VoicevoxApiClient.CreateForAvisSpeech();
    }

    [TearDown]
    public void TearDown()
    {
        _client.Dispose();
    }

    protected async ValueTask PlaySoundAsync(byte[] wav)
    {
        await using var stream = new MemoryStream(wav);
        await PlaySoundAsync(stream);
    }

    protected async ValueTask PlaySoundAsync(Stream stream)
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

    #endregion

    [Test, Timeout(10000)]
    public async Task PostSynthesisAsyncTest()
    {
        var styleId = await GetDefaultStyleIdAsync();

        // この結果を使って合成する
        var audioQuery = await _client.CreateAudioQueryAsync("こんにちは、世界！", styleId);
        Assert.IsNotNull(audioQuery);

        var result = await _client.SynthesisAsync(styleId, audioQuery);

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
        var aq1 = await _client.CreateAudioQueryAsync("そのいち", styleId);
        var aq2 = await _client.CreateAudioQueryAsync("そのに", styleId + 1);
        Assert.IsNotNull(aq1);
        Assert.IsNotNull(aq2);

        var audioQueries = new[] { aq1, aq2 };

        var zip = await _client.MultiSpeakerSynthesisAsync(styleId, audioQueries);
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

    private async ValueTask<int> GetDefaultStyleIdAsync()
    {
        var speakers = await _client.GetSpeakersAsync();
        return speakers[0].Styles[0].Id;
    }
}