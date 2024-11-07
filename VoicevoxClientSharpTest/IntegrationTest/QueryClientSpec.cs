using VoicevoxClientSharp.ApiClient;

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
}