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
        catch (VoicevoxApiErrorException e)
        {
            var j = JObject.Parse(e.Message);
            var actual = j["detail"]!.ToString();
            var expected = "該当するプリセットIDが見つかりません";
            Assert.That(actual, Is.EqualTo(expected));
        }
    }

    [Test, Timeout(15000)]
    public async Task PostSingFrameAudioQueryAsyncTest()
    {
        var notes = new Notes(
            new Note(key: null, frameLength: 15, lyric: "", id: null),
            new Note(key: 60, frameLength: 45, lyric: "ド", id: null),
            new Note(key: 62, frameLength: 45, lyric: "レ", id: null),
            new Note(key: null, frameLength: 15, lyric: "", id: null)
        );
        var result = await _queryClient.PostSingFrameAudioQueryAsync(notes, 6000);
        Assert.IsNotNull(result);
    }

    [Test, Timeout(5000)]
    public async Task PostAccentPhraseAsyncTest()
    {
        var result = await _queryClient.PostAccentPhraseAsync("こんにちは、世界！", 0);
        Assert.IsNotNull(result);
    }

    [Test, Timeout(20000)]
    public async Task PostMoraAsyncTests()
    {
        // ここで得た結果を使ってテストを続ける
        var accentPhrases = await _queryClient.PostAccentPhraseAsync("こんにちは、世界！", 0);
        Assert.IsNotNull(accentPhrases);

        {
            var result = await _queryClient.PostMoraDataAsync(0, accentPhrases);
            Assert.IsNotNull(result);
        }
    }
}