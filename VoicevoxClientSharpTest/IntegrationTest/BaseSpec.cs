/*
    NAudio Copyright 2020 Mark Heath
    https://github.com/naudio/NAudio/blob/master/license.txt
*/

using NAudio.Wave;
using VoicevoxClientSharp.ApiClient;

namespace VoicevoxClientSharpTest.IntegrationTest;

public class BaseSpec
{
    private IVoicevoxRawApiClient _voicevoxRawApiClient;
    protected IQueryClient QueryClient { get; private set; }
    protected ISynthesisClient SynthesisClient { get; private set; }
    protected IMiscClient MiscClient { get; private set; }
    protected ISpeakerClient SpeakerClient { get; private set; }
    protected IPresetClient PresetClient { get; private set; }
    protected ILibraryClient LibraryClient { get; private set; }
    protected IUserDictionaryClient UserDictionaryClient { get; private set; }

    [SetUp]
    public void Setup()
    {
        _voicevoxRawApiClient = new VoicevoxRawApiClient("http://localhost:50021");
        QueryClient = _voicevoxRawApiClient;
        SynthesisClient = _voicevoxRawApiClient;
        MiscClient = _voicevoxRawApiClient;
        SpeakerClient = _voicevoxRawApiClient;
        PresetClient = _voicevoxRawApiClient;
        LibraryClient = _voicevoxRawApiClient;
        UserDictionaryClient = _voicevoxRawApiClient;
    }

    [TearDown]
    public void TearDown()
    {
        _voicevoxRawApiClient.Dispose();
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
}