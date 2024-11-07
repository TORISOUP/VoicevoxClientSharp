using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace VoicevoxClientSharp.Models
{
    /// <summary>
    /// ParseKanaBadRequest
    /// </summary>
    [DataContract(Name = "ParseKanaBadRequest")]
    public sealed class ParseKanaBadRequest : IEquatable<ParseKanaBadRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParseKanaBadRequest" /> class.
        /// </summary>
        /// <param name="text">エラーメッセージ (required).</param>
        /// <param name="errorName">
        /// エラー名  |name|description| |- --|- --| | UNKNOWN_TEXT | 判別できない読み仮名があります: {text} | | ACCENT_TOP |
        /// 句頭にアクセントは置けません: {text} | | ACCENT_TWICE | 1つのアクセント句に二つ以上のアクセントは置けません: {text} | | ACCENT_NOTFOUND |
        /// アクセントを指定していないアクセント句があります: {text} | | EMPTY_PHRASE | {position}番目のアクセント句が空白です | | INTERROGATION_MARK_NOT_AT_END |
        /// アクセント句末以外に「？」は置けません: {text} | | INFINITE_LOOP | 処理時に無限ループになってしまいました...バグ報告をお願いします。 | (required).
        /// </param>
        /// <param name="errorArgs">エラーを起こした箇所 (required).</param>
        public ParseKanaBadRequest(
            string text,
            string errorName,
            Dictionary<string, string> errorArgs)
        {
            Text = text;
            ErrorName = errorName;
            ErrorArgs = errorArgs;
        }

        /// <summary>
        /// エラーメッセージ
        /// </summary>
        /// <value>エラーメッセージ</value>
        [DataMember(Name = "text", IsRequired = true, EmitDefaultValue = false)]
        public string Text { get; set; }

        /// <summary>
        /// エラー名  |name|description| |- --|- --| | UNKNOWN_TEXT | 判別できない読み仮名があります: {text} | | ACCENT_TOP | 句頭にアクセントは置けません: {text} |
        /// | ACCENT_TWICE | 1つのアクセント句に二つ以上のアクセントは置けません: {text} | | ACCENT_NOTFOUND | アクセントを指定していないアクセント句があります: {text} | |
        /// EMPTY_PHRASE | {position}番目のアクセント句が空白です | | INTERROGATION_MARK_NOT_AT_END | アクセント句末以外に「？」は置けません: {text} | |
        /// INFINITE_LOOP | 処理時に無限ループになってしまいました...バグ報告をお願いします。 |
        /// </summary>
        /// <value>
        /// エラー名  |name|description| |- --|- --| | UNKNOWN_TEXT | 判別できない読み仮名があります: {text} | | ACCENT_TOP | 句頭にアクセントは置けません:
        /// {text} | | ACCENT_TWICE | 1つのアクセント句に二つ以上のアクセントは置けません: {text} | | ACCENT_NOTFOUND | アクセントを指定していないアクセント句があります: {text} | |
        /// EMPTY_PHRASE | {position}番目のアクセント句が空白です | | INTERROGATION_MARK_NOT_AT_END | アクセント句末以外に「？」は置けません: {text} | |
        /// INFINITE_LOOP | 処理時に無限ループになってしまいました...バグ報告をお願いします。 |
        /// </value>
        [DataMember(Name = "error_name", IsRequired = true, EmitDefaultValue = false)]
        public string ErrorName { get; set; }

        /// <summary>
        /// エラーを起こした箇所
        /// </summary>
        /// <value>エラーを起こした箇所</value>
        [DataMember(Name = "error_args", IsRequired = true, EmitDefaultValue = false)]
        public Dictionary<string, string> ErrorArgs { get; set; }

        public bool Equals(ParseKanaBadRequest? other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Text == other.Text && ErrorName == other.ErrorName && ErrorArgs.Equals(other.ErrorArgs);
        }


        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ParseKanaBadRequest {\n");
            sb.Append("  Text: ").Append(Text).Append("\n");
            sb.Append("  ErrorName: ").Append(ErrorName).Append("\n");
            sb.Append("  ErrorArgs: ").Append(ErrorArgs).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        public override bool Equals(object? obj)
        {
            return ReferenceEquals(this, obj) || (obj is ParseKanaBadRequest other && Equals(other));
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Text.GetHashCode();
                hashCode = (hashCode * 397) ^ ErrorName.GetHashCode();
                hashCode = (hashCode * 397) ^ ErrorArgs.GetHashCode();
                return hashCode;
            }
        }
    }
}