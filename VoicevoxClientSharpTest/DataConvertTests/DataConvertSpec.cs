using System.Text.Json;
using VoicevoxClientSharp.ApiClient.Models;

namespace VoicevoxClientSharpTest.DataConvertTests;

public class DataConvertSpec
{
    [Test]
    public void Speakerを変換できる()
    {
        var json = File.ReadAllText("./Resources/Speakers.json");

        var speaker = JsonSerializer.Deserialize<Speaker[]>(json);

        Assert.IsNotNull(speaker);
        Assert.That(speaker.Length, Is.EqualTo(31));
        Assert.That(speaker[0].Name, Is.EqualTo("四国めたん"));
        
        Assert.That(
            speaker[0].Styles.FirstOrDefault(x => x.Id == 0)?.Type,
            Is.EqualTo(SpeakerType.Talk));
        
        Assert.That(
            speaker[0].Styles.FirstOrDefault(x => x.Id == 2)?.Type,
            Is.EqualTo(SpeakerType.SingingTeacher));
        
        Assert.That(
            speaker[0].Styles.FirstOrDefault(x => x.Id == 4)?.Type,
            Is.EqualTo(SpeakerType.FrameDecode));
        
        Assert.That(
            speaker[0].Styles.FirstOrDefault(x => x.Id == 6)?.Type,
            Is.EqualTo(SpeakerType.Sing));
    }
}