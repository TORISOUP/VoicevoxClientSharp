using Newtonsoft.Json.Linq;
using VoicevoxClientSharp.ApiClient;

namespace VoicevoxClientSharpTest.IntegrationTest.Voicevox;

public class QueryClientSpec : BaseSpec
{
    [Test, Timeout(5000)]
    public async Task CreateAudioQueryAsyncTest()
    {
        var styleId = await GetDefaultStyleIdAsync();

        
        var result = await QueryClient.CreateAudioQueryAsync("こんにちは、世界！", styleId);
        Assert.IsNotNull(result);
        Assert.That(result.AccentPhrases.Count, Is.GreaterThan(0));
        Assert.That(result.AccentPhrases[0].Moras.Count, Is.GreaterThan(0));
    }

    [Test, Timeout(5000)]
    public async Task CreateAudioQueryFromPresetAsyncTest()
    {
        try
        {
            var styleId = await GetDefaultStyleIdAsync();

            var result = await QueryClient.CreateAudioQueryFromPresetAsync("こんにちは、世界！", styleId);
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


    [Test, Timeout(5000)]
    public async Task CreateAccentPhraseAsyncTest()
    {
        var styleId = await GetDefaultStyleIdAsync();

        var result = await QueryClient.CreateAccentPhraseAsync("こんにちは、世界！", styleId);
        Assert.IsNotNull(result);
    }

    [Test, Timeout(20000)]
    public async Task CreateMoraAsyncTests()
    {
        var styleId = await GetDefaultStyleIdAsync();

        // ここで得た結果を使ってテストを続ける
        var accentPhrases = await QueryClient.CreateAccentPhraseAsync("こんにちは、世界！", styleId);
        Assert.IsNotNull(accentPhrases);
        Assert.IsNotNull(accentPhrases[0].Moras);

        {
            var result = await QueryClient.FetchMoraDataAsync(0, accentPhrases);
            Assert.IsNotNull(result);
            Assert.That(result.Length, Is.GreaterThan(0));
            Assert.IsNotNull(result[0].Moras);
        }
        {
            var result = await QueryClient.FetchMoraLengthAsync(0, accentPhrases);
            Assert.IsNotNull(result);
            Assert.That(result.Length, Is.GreaterThan(0));
            Assert.IsNotNull(result[0].Moras);
        }
        {
            var result = await QueryClient.FetchMoraPitchAsync(0, accentPhrases);
            Assert.IsNotNull(result);
            Assert.That(result.Length, Is.GreaterThan(0));
            Assert.IsNotNull(result[0].Moras);
        }
    }
}