/*
    NAudio Copyright 2020 Mark Heath
    https://github.com/naudio/NAudio/blob/master/license.txt
*/

using NAudio.Wave;
using VoicevoxClientSharp.ApiClient;
using VoicevoxClientSharp.ApiClient.Models;

namespace VoicevoxClientSharpTest.IntegrationTest.Voicevox;

public class BaseSpec
{
    private IVoicevoxApiClient _voicevoxApiClient;
    protected IQueryClient<AudioQuery> QueryClient { get; private set; }
    protected ISynthesisClient<AudioQuery> SynthesisClient { get; private set; }
    protected IMiscClient MiscClient { get; private set; }
    protected ISpeakerClient SpeakerClient { get; private set; }
    protected IPresetClient PresetClient { get; private set; }
    protected ILibraryClient LibraryClient { get; private set; }
    protected IUserDictionaryClient UserDictionaryClient { get; private set; }
    
    protected ISingClient SingClient { get; private set; }

    [SetUp]
    public void Setup()
    {
        _voicevoxApiClient = new VoicevoxApiClient("http://localhost:50021");
        QueryClient = _voicevoxApiClient;
        SynthesisClient = _voicevoxApiClient;
        MiscClient = _voicevoxApiClient;
        SpeakerClient = _voicevoxApiClient;
        PresetClient = _voicevoxApiClient;
        LibraryClient = _voicevoxApiClient;
        UserDictionaryClient = _voicevoxApiClient;
        SingClient = _voicevoxApiClient;
    }

    [TearDown]
    public void TearDown()
    {
        _voicevoxApiClient.Dispose();
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
    
    protected async ValueTask<int> GetDefaultStyleIdAsync()
    {
        var speakers = await SpeakerClient.GetSpeakersAsync();
        return speakers[0].Styles[0].Id;
    }
}