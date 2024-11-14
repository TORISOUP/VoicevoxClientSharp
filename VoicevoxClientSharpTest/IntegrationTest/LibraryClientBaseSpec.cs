namespace VoicevoxClientSharpTest.IntegrationTest;

public class LibraryClientBaseSpec : BaseSpec
{
    [Test, Timeout(5000), Ignore("Internal Server Errorが返るのでスキップ")]
    public async Task GetDownloadableLibrariesAsyncTest()
    {
        var result = await LibraryClient.GetDownloadableLibrariesAsync();
        // Assert
        Assert.NotNull(result);
    }
}