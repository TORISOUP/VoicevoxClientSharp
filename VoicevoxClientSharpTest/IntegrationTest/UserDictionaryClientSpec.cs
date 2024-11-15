using VoicevoxClientSharp.Models;

namespace VoicevoxClientSharpTest.IntegrationTest;

public class UserDictionaryClientSpec : BaseSpec
{
    [Test, Timeout(10000)]
    public async Task UserDictionaryWordsAsync()
    {
        var surface = "テストワード登録";
        var pronunciation = "テストワードトウロク";
        var accentType = 1;
        var wordTypes = WordTypes.Propernoun;
        var priority = 10;

        // 登録する
        var id = await UserDictionaryClient.AddUserDictionaryWordAsync(
            surface, pronunciation, accentType, wordTypes, priority);
        Assert.IsNotEmpty(id);

        // 登録されているか確認
        var registeredWords = await UserDictionaryClient.GetUserDictionaryWordsAsync();
        Assert.IsTrue(registeredWords.ContainsKey(id));
        Assert.That(registeredWords[id].Surface, Is.EqualTo(surface));
        Assert.That(registeredWords[id].Pronunciation, Is.EqualTo(pronunciation));
        Assert.That(registeredWords[id].AccentType, Is.EqualTo(accentType));
        Assert.That(registeredWords[id].PartOfSpeech, Is.EqualTo("名詞"));
        Assert.That(registeredWords[id].PartOfSpeechDetail1, Is.EqualTo("固有名詞"));
        Assert.That(registeredWords[id].Priority, Is.EqualTo(priority));

        // 更新する
        var updatedSurface = "テストワード更新";
        var updatedPronunciation = "テストワードコウシン";
        var updatedAccentType = 2;
        var updatedPriority = 5;
        await UserDictionaryClient.UpdateUserDictionaryWordAsync(
            id, updatedSurface, updatedPronunciation, updatedAccentType, null, updatedPriority);

        // 更新されているか確認
        registeredWords = await UserDictionaryClient.GetUserDictionaryWordsAsync();

        Assert.IsTrue(registeredWords.ContainsKey(id));
        Assert.That(registeredWords[id].Surface, Is.EqualTo(updatedSurface));
        Assert.That(registeredWords[id].Pronunciation, Is.EqualTo(updatedPronunciation));
        Assert.That(registeredWords[id].AccentType, Is.EqualTo(updatedAccentType));
        Assert.That(registeredWords[id].PartOfSpeech, Is.EqualTo("名詞"));
        Assert.That(registeredWords[id].PartOfSpeechDetail1, Is.EqualTo("固有名詞"));
        Assert.That(registeredWords[id].Priority, Is.EqualTo(updatedPriority));

        // 後で使うので取っておく
        var wordList = registeredWords;

        // 削除する
        await UserDictionaryClient.DeleteUserDictionaryWordAsync(id);

        // 削除されているか確認
        registeredWords = await UserDictionaryClient.GetUserDictionaryWordsAsync();
        // 削除されている
        Assert.IsFalse(registeredWords.ContainsKey(id));

        // インポートする
        await UserDictionaryClient.ImportUserDictionaryWordsAsync(wordList, true);

        // インポートされているか確認
        registeredWords = await UserDictionaryClient.GetUserDictionaryWordsAsync();
        Assert.IsTrue(registeredWords.ContainsKey(id));
        Assert.That(registeredWords[id].Surface, Is.EqualTo(updatedSurface));
        Assert.That(registeredWords[id].Pronunciation, Is.EqualTo(updatedPronunciation));
        Assert.That(registeredWords[id].AccentType, Is.EqualTo(updatedAccentType));
        Assert.That(registeredWords[id].PartOfSpeech, Is.EqualTo("名詞"));
        Assert.That(registeredWords[id].PartOfSpeechDetail1, Is.EqualTo("固有名詞"));
        Assert.That(registeredWords[id].Priority, Is.EqualTo(updatedPriority));

        // 削除する
        await UserDictionaryClient.DeleteUserDictionaryWordAsync(id);
        registeredWords = await UserDictionaryClient.GetUserDictionaryWordsAsync();
        Assert.IsFalse(registeredWords.ContainsKey(id));
    }
}