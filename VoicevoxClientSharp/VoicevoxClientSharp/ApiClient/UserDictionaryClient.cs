using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using VoicevoxClientSharp.Models;

namespace VoicevoxClientSharp.ApiClient
{
    public interface IUserDictionaryClient : IDisposable
    {
        /// <summary>
        /// GET /user_dict
        /// ユーザー辞書に登録されている単語の一覧を返します。 単語の表層形(surface)は正規化済みの物を返します。
        /// </summary>
        /// <returns></returns>
        ValueTask<IReadOnlyDictionary<string, UserDictWord>>
            GetUserDictionaryWordsAsync(CancellationToken ct = default);


        /// <summary>
        /// POST /user_dict_word
        /// ユーザー辞書に言葉を追加します。
        /// </summary>
        /// <param name="surface">言葉の表層形</param>
        /// <param name="pronunciation">言葉の発音（カタカナ）</param>
        /// <param name="accentType">アクセント型（音が下がる場所を指す）</param>
        /// <param name="wordTypes">PROPER_NOUN（固有名詞）、COMMON_NOUN（普通名詞）、VERB（動詞）、ADJECTIVE（形容詞）、SUFFIX（語尾）のいずれか</param>
        /// <param name="priority">単語の優先度（0から10までの整数）。数字が大きいほど優先度が高くなる。1から9までの値を指定することを推奨</param>
        /// <param name="ct"></param>
        /// <returns>WordUuid</returns>
        ValueTask<string> AddUserDictionaryWordAsync(
            string surface,
            string pronunciation,
            int accentType,
            WordTypes? wordTypes = null,
            int? priority = 5,
            CancellationToken ct = default
        );

        /// <summary>
        /// POST /user_dict_word/{word_uuid}
        /// ユーザー辞書に登録されている言葉を更新します。
        /// </summary>
        /// <param name="wordUuid">更新する言葉のUUID</param>
        /// <param name="surface">言葉の表層形</param>
        /// <param name="pronunciation">言葉の発音（カタカナ）</param>
        /// <param name="accentType">アクセント型（音が下がる場所を指す）</param>
        /// <param name="wordTypes">PROPER_NOUN（固有名詞）、COMMON_NOUN（普通名詞）、VERB（動詞）、ADJECTIVE（形容詞）、SUFFIX（語尾）のいずれか</param>
        /// <param name="priority">単語の優先度（0から10までの整数）。数字が大きいほど優先度が高くなる。1から9までの値を指定することを推奨</param>
        /// <param name="ct"></param>
        ValueTask UpdateUserDictionaryWordAsync(
            string wordUuid,
            string surface,
            string pronunciation,
            int accentType,
            WordTypes? wordTypes = null,
            int? priority = 5,
            CancellationToken ct = default
        );

        /// <summary>
        /// DELETE /user_dict_word/{word_uuid}
        /// ユーザー辞書に登録されている言葉を削除します。
        /// </summary>
        /// <param name="wordUuid">削除する言葉のUUID</param>
        /// <param name="ct"></param>
        ValueTask DeleteUserDictionaryWordAsync(
            string wordUuid,
            CancellationToken ct = default
        );

        /// <summary>
        /// POST /import_user_dict
        /// 他のユーザー辞書をインポートします。
        /// </summary>
        /// <param name="words"></param>
        /// <param name="overrideEntry">重複したエントリがあった場合、上書きするかどうか</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        ValueTask ImportUserDictionaryWordsAsync(
            IReadOnlyDictionary<string, UserDictWord> words,
            bool overrideEntry,
            CancellationToken ct = default
        );
    }

    public partial class RawApiClient
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async ValueTask<IReadOnlyDictionary<string, UserDictWord>> GetUserDictionaryWordsAsync(
            CancellationToken ct = default)
        {
            var url = $"{_baseUrl}/user_dict";
            return await GetAsync<IReadOnlyDictionary<string, UserDictWord>>(url, ct);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public ValueTask<string> AddUserDictionaryWordAsync(string surface,
            string pronunciation,
            int accentType,
            WordTypes? wordTypes = null,
            int? priority = 0,
            CancellationToken ct = default)
        {
            var queryString = QueryString(
                ("surface", surface),
                ("pronunciation", pronunciation),
                ("accent_type", accentType.ToString()),
                ("word_types", ToRequest(wordTypes)),
                ("priority", priority?.ToString())
            );

            var url = $"{_baseUrl}/user_dict_word?{queryString}";
            return PostAsync<string>(url, ct);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public ValueTask UpdateUserDictionaryWordAsync(string wordUuid,
            string surface,
            string pronunciation,
            int accentType,
            WordTypes? wordTypes = null,
            int? priority = 5,
            CancellationToken ct = default)
        {
            var queryString = QueryString(
                ("word_uuid ", wordUuid),
                ("surface", surface),
                ("pronunciation", pronunciation),
                ("accent_type", accentType.ToString()),
                ("word_types", ToRequest(wordTypes)),
                ("priority", priority?.ToString())
            );

            var url = $"{_baseUrl}/user_dict_word/{wordUuid}?{queryString}";
            return PutAsync(url, ct);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async ValueTask DeleteUserDictionaryWordAsync(string wordUuid, CancellationToken ct = default)
        {
            var url = $"{_baseUrl}/user_dict_word/{wordUuid}";
            var response = await _httpClient.DeleteAsync(url, ct);
            if ((int)response.StatusCode >= 400)
            {
                var errorJson = await response.Content.ReadAsStringAsync();
                throw new VoicevoxApiErrorException(errorJson, errorJson, (int)response.StatusCode);
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async ValueTask ImportUserDictionaryWordsAsync(
            IReadOnlyDictionary<string, UserDictWord> words,
            bool overrideEntry,
            CancellationToken ct = default)
        {
            var queryString = QueryString(("override", overrideEntry.ToString()));
            
            var url = $"{_baseUrl}/import_user_dict?{queryString}";

            var requestJson = JsonSerializer.Serialize(words, _jsonSerializerOptions);
            var content = new StringContent(requestJson, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, content, ct);
            if ((int)response.StatusCode >= 400)
            {
                var errorJson = await response.Content.ReadAsStringAsync();
                throw new VoicevoxApiErrorException(errorJson, errorJson, (int)response.StatusCode);
            }
        }

        private string? ToRequest(WordTypes? wordTypes)
        {
            return wordTypes switch
            {
                WordTypes.Propernoun => "PROPER_NOUN",
                WordTypes.Commonnoun => "COMMON_NOUN",
                WordTypes.Verb => "VERB",
                WordTypes.Adjective => "ADJECTIVE",
                WordTypes.Suffix => "SUFFIX",
                _ => null
            };
        }
    }
}