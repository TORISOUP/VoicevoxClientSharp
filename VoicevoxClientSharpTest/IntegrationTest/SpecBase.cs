using NAudio.Wave;
using VoicevoxClientSharp.ApiClient;

namespace VoicevoxClientSharpTest.IntegrationTest;

public class SpecBase
{
    private IVoicevoxApiClient _voicevoxApiClient;
    protected IQueryClient QueryClient;
    protected ISynthesisClient SynthesisClient;
    protected IMiscClient MiscClient;
    protected ISpeakerClient SpeakerClient;
    protected IPresetClient PresetClient;

    [SetUp]
    public void Setup()
    {
        _voicevoxApiClient = new RawApiClient();
        QueryClient = _voicevoxApiClient;
        SynthesisClient = _voicevoxApiClient;
        MiscClient = _voicevoxApiClient;
        SpeakerClient = _voicevoxApiClient;
        PresetClient = _voicevoxApiClient;
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