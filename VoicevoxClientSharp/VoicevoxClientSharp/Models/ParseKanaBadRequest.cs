using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;


namespace VoicevoxClientSharp.Models
{
    public sealed class ParseKanaBadRequest : IEquatable<ParseKanaBadRequest>
    {
        [JsonConstructor]
        public ParseKanaBadRequest()
        {
        }

        /// <summary>
        /// エラーメッセージ
        /// </summary>
        /// <value>エラーメッセージ</value>
        [JsonPropertyName("text")]
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
        [JsonPropertyName("error_name")]
        public string ErrorName { get; set; }

        /// <summary>
        /// エラーを起こした箇所
        /// </summary>
        /// <value>エラーを起こした箇所</value>
        [JsonPropertyName("error_args")]
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