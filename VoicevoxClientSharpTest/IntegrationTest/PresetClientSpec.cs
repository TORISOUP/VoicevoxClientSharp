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
            speedScale: 1.0M,
            pitchScale: 0M,
            intonationScale: 1.0M,
            volumeScale: 1.0M,
            prePhonemeLength: 0.1M,
            postPhonemeLength: 1.0M,
            pauseLength: 0.5M,
            pauseLengthScale: 1.0M);


        // 追加
        var createdId = await PresetClient.AddPresetAsync(newPreset);

        // 作成成功
        Assert.That(createdId, Is.EqualTo(presetId));

        // プリセットを取得する
        {
            var ps = await PresetClient.GetPresetsAsync();
            Assert.IsNotNull(ps);

            // 追加したやつが存在する
            var addedPreset = ps.FirstOrDefault(p => p.Id == presetId);
            Assert.IsNotNull(addedPreset);
            Assert.That(addedPreset, Is.EqualTo(newPreset));
        }

        var updatedPreset = new Preset(
            id: presetId,
            name: "TestPresetUpdated",
            speakerUuid: speakerId,
            styleId: styleId,
            speedScale: 2.0M,
            pitchScale: 1M,
            intonationScale: 2.0M,
            volumeScale: 2.0M,
            prePhonemeLength: 0.5M,
            postPhonemeLength: 2.0M,
            pauseLength: 0.1M,
            pauseLengthScale: 2.0M
        );

        // 更新
        var updatedId = await PresetClient.UpdatePresetAsync(updatedPreset);
        
        // 更新成功
        Assert.That(updatedId, Is.EqualTo(presetId));
        {
            var ps = await PresetClient.GetPresetsAsync();
            Assert.IsNotNull(ps);

            // 更新されているはず
            var addedPreset = ps.FirstOrDefault(p => p.Id == presetId);
            Assert.IsNotNull(addedPreset);
            Assert.That(addedPreset, Is.EqualTo(updatedPreset));
        }
        
        // 削除
        await PresetClient.DeletePresetAsync(presetId);
        
        // 削除成功
        {
            var ps = await PresetClient.GetPresetsAsync();
            Assert.IsNotNull(ps);

            // 削除されているはず
            var addedPreset = ps.FirstOrDefault(p => p.Id == presetId);
            Assert.IsNull(addedPreset);
        }
    }
}