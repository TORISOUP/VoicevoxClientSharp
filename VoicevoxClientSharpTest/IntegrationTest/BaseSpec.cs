using NAudio.Wave;
using VoicevoxClientSharp.ApiClient;

namespace VoicevoxClientSharpTest.IntegrationTest;

public class BaseSpec
{
    private IVoicevoxApiClient _voicevoxApiClient;
    protected IQueryClient QueryClient { get; private set; }
    protected ISynthesisClient SynthesisClient { get; private set; }
    protected IMiscClient MiscClient { get; private set; }
    protected ISpeakerClient SpeakerClient { get; private set; }
    protected IPresetClient PresetClient { get; private set; }
    protected ILibraryClient LibraryClient { get; private set; }

    [SetUp]
    public void Setup()
    {
        _voicevoxApiClient = new RawApiClient();
        QueryClient = _voicevoxApiClient;
        SynthesisClient = _voicevoxApiClient;
        MiscClient = _voicevoxApiClient;
        SpeakerClient = _voicevoxApiClient;
        PresetClient = _voicevoxApiClient;
        LibraryClient = _voicevoxApiClient;
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
}