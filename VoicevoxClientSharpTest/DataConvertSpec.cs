using Newtonsoft.Json;
using VoicevoxClientSharp.Models;

namespace VoicevoxClientSharpTest;

public class DataConvertSpec
{
    [Test]
    public void ConvertHttpValidationError()
    {
        var error =
            @"{""detail"":[{""type"":""missing"",""loc"":[""query"",""speaker""],""msg"":""Field required"",""input"":null}]}";

        var result = JsonConvert.DeserializeObject<HttpValidationError>(error);

        Assert.That(result, Is.Not.Null);
        var detail = result!.Detail;
        Assert.That(detail, Is.Not.Null);
        Assert.That(detail.Length, Is.EqualTo(1));
        Assert.That(detail[0].Type, Is.EqualTo("missing"));
        Assert.That(detail[0].Loc, Is.EquivalentTo(new object[] { "query", "speaker" }));
        Assert.That(detail[0].Msg, Is.EqualTo("Field required"));
        Assert.That(detail[0].Input, Is.Null);
    }
}