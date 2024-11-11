using VoicevoxClientSharp.Models;

namespace VoicevoxClientSharpTest.IntegrationTest;

public class PresetClientSpec : SpecBase
{
    [Test, Timeout(15000)]
    public async Task PresetsAsyncTest()
    {
        var speaker = await SpeakerClient.GetSpeakersAsync();
        Assert.IsNotNull(speaker);
        var speakerId = speaker[0].SpeakerUuid;
        var styleId = speaker[0].Styles[0].Id;


        var initialPreset = await PresetClient.GetPresetsAsync();
        Assert.IsNotNull(initialPreset);

        // リストに存在しないIdを定義する
        var presetId = initialPreset.Max(p => p.Id) + 1;

        // // プリセットを追加する
        var newPreset = new Preset(
            id: presetId,
            name: "TestPreset",
            speakerUuid: speakerId,
            styleId: styleId,
            speedScale: 1M,
            pitchScale: 0M,
            intonationScale: 1M,
            volumeScale: 1M,
            prePhonemeLength: 0.1M,
            postPhonemeLength: 1.0M);
        
        // TODO: 壊れている
        
        // 追加
        await PresetClient.AddPresetAsync(newPreset);
        
        // プリセットを取得する
        {
            var ps = await PresetClient.GetPresetsAsync();
            Assert.IsNotNull(ps);

            // 追加したやつが存在する
            var addedPreset = ps.FirstOrDefault(p => p.Id == presetId);
            Assert.IsNotNull(addedPreset);
            Assert.That(addedPreset, Is.EqualTo(newPreset));
        }
    }
}