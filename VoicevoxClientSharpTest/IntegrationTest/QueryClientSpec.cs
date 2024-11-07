using Newtonsoft.Json.Linq;
using VoicevoxClientSharp.ApiClient;
using VoicevoxClientSharp.Models;

namespace VoicevoxClientSharpTest.IntegrationTest;

public class QueryClientSpec
{
    private IQueryClient _queryClient;

    [SetUp]
    public void Setup()
    {
        _queryClient = new RawApiClient();
    }

    [TearDown]
    public void TearDown()
    {
        _queryClient.Dispose();
    }

    [Test, Timeout(5000)]
    public async Task PostAudioQueryAsyncTest()
    {
        var result = await _queryClient.PostAudioQueryAsync("こんにちは、世界！", 0);
        Assert.IsNotNull(result);
    }

    [Test, Timeout(5000)]
    public async Task PostAudioQueryFromPresetAsyncTest()
    {
        try
        {
            var result = await _queryClient.PostAudioQueryFromPresetAsync("こんにちは、世界！", 0);
            Assert.IsNotNull(result);
        }
        catch (VoicevoxHttpValidationErrorException e)
        {
            var j = JObject.Parse(e.Error.Json);
            var actual = j["detail"]!.ToString();
            var expected = "該当するプリセットIDが見つかりません";
            Assert.That(actual, Is.EqualTo(expected));
        }
    }

    [Test, Timeout(5000)]
    public async Task PostSingFrameAudioQueryAsyncTest()
    {
        var note = new Note(
            "1", 1, 1, "こんにちは"
        );
        var result = await _queryClient.PostSingFrameAudioQueryAsync(new Notes(note), 0);
        Assert.IsNotNull(result);
    }
}