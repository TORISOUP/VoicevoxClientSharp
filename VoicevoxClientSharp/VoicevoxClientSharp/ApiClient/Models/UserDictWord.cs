using System;
using System.Text;
using System.Text.Json.Serialization;

namespace VoicevoxClientSharp.Models
{
    /// <summary>
    /// 辞書のコンパイルに使われる情報
    /// </summary>
    public sealed class UserDictWord : IEquatable<UserDictWord>
    {
        [JsonConstructor]
        public UserDictWord()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserDictWord" /> class.
        /// </summary>
        /// <param name="surface">表層形 (required).</param>
        /// <param name="priority">優先度 (required).</param>
        /// <param name="contextId">文脈ID (default to 1348).</param>
        /// <param name="partOfSpeech">品詞 (required).</param>
        /// <param name="partOfSpeechDetail1">品詞細分類1 (required).</param>
        /// <param name="partOfSpeechDetail2">品詞細分類2 (required).</param>
        /// <param name="partOfSpeechDetail3">品詞細分類3 (required).</param>
        /// <param name="inflectionalType">活用型 (required).</param>
        /// <param name="inflectionalForm">活用形 (required).</param>
        /// <param name="stem">原形 (required).</param>
        /// <param name="yomi">読み (required).</param>
        /// <param name="pronunciation">発音 (required).</param>
        /// <param name="accentType">アクセント型 (required).</param>
        /// <param name="moraCount">モーラ数.</param>
        /// <param name="accentAssociativeRule">アクセント結合規則 (required).</param>
        public UserDictWord(string surface,
            int priority,
            string partOfSpeech,
            string partOfSpeechDetail1,
            string partOfSpeechDetail2,
            string partOfSpeechDetail3,
            string inflectionalType,
            string inflectionalForm,
            string stem,
            string yomi,
            string pronunciation,
            int accentType,
            int? moraCount,
            string accentAssociativeRule,
            int contextId = 1348)
        {
            Surface = surface;
            Priority = priority;
            PartOfSpeech = partOfSpeech;
            PartOfSpeechDetail1 = partOfSpeechDetail1;
            PartOfSpeechDetail2 = partOfSpeechDetail2;
            PartOfSpeechDetail3 = partOfSpeechDetail3;
            InflectionalType = inflectionalType;
            InflectionalForm = inflectionalForm;
            Stem = stem;
            Yomi = yomi;
            Pronunciation = pronunciation;
            AccentType = accentType;
            AccentAssociativeRule = accentAssociativeRule;
            ContextId = contextId;
            MoraCount = moraCount;
        }

        /// <summary>
        /// 表層形
        /// </summary>
        /// <value>表層形</value>
        [JsonPropertyName("surface")]
        public string Surface { get; set; }

        /// <summary>
        /// 優先度
        /// </summary>
        /// <value>優先度</value>
        [JsonPropertyName("priority")]
        public int Priority { get; set; }

        /// <summary>
        /// 文脈ID
        /// </summary>
        /// <value>文脈ID</value>
        [JsonPropertyName("context_id")]
        public int ContextId { get; set; }

        /// <summary>
        /// 品詞
        /// </summary>
        /// <value>品詞</value>
        [JsonPropertyName("part_of_speech")]
        public string PartOfSpeech { get; set; }

        /// <summary>
        /// 品詞細分類1
        /// </summary>
        /// <value>品詞細分類1</value>
        [JsonPropertyName("part_of_speech_detail_1")]
        public string PartOfSpeechDetail1 { get; set; }

        /// <summary>
        /// 品詞細分類2
        /// </summary>
        /// <value>品詞細分類2</value>
        [JsonPropertyName("part_of_speech_detail_2")]
        public string PartOfSpeechDetail2 { get; set; }

        /// <summary>
        /// 品詞細分類3
        /// </summary>
        /// <value>品詞細分類3</value>
        [JsonPropertyName("part_of_speech_detail_3")]
        public string PartOfSpeechDetail3 { get; set; }

        /// <summary>
        /// 活用型
        /// </summary>
        /// <value>活用型</value>
        [JsonPropertyName("inflectional_type")]
        public string InflectionalType { get; set; }

        /// <summary>
        /// 活用形
        /// </summary>
        /// <value>活用形</value>
        [JsonPropertyName("inflectional_form")]
        public string InflectionalForm { get; set; }

        /// <summary>
        /// 原形
        /// </summary>
        /// <value>原形</value>
        [JsonPropertyName("stem")]
        public string Stem { get; set; }

        /// <summary>
        /// 読み
        /// </summary>
        /// <value>読み</value>
        [JsonPropertyName("yomi")]
        public string Yomi { get; set; }

        /// <summary>
        /// 発音
        /// </summary>
        /// <value>発音</value>
        [JsonPropertyName("pronunciation")]
        public string Pronunciation { get; set; }

        /// <summary>
        /// アクセント型
        /// </summary>
        /// <value>アクセント型</value>
        [JsonPropertyName("accent_type")]
        public int AccentType { get; set; }

        /// <summary>
        /// モーラ数
        /// </summary>
        /// <value>モーラ数</value>
        [JsonPropertyName("mora_count")]
        public int? MoraCount { get; set; }

        /// <summary>
        /// アクセント結合規則
        /// </summary>
        /// <value>アクセント結合規則</value>
        [JsonPropertyName("accent_associative_rule")]
        public string AccentAssociativeRule { get; set; }

        public bool Equals(UserDictWord? other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Surface == other.Surface && Priority == other.Priority && ContextId == other.ContextId &&
                   PartOfSpeech == other.PartOfSpeech && PartOfSpeechDetail1 == other.PartOfSpeechDetail1 &&
                   PartOfSpeechDetail2 == other.PartOfSpeechDetail2 &&
                   PartOfSpeechDetail3 == other.PartOfSpeechDetail3 && InflectionalType == other.InflectionalType &&
                   InflectionalForm == other.InflectionalForm && Stem == other.Stem && Yomi == other.Yomi &&
                   Pronunciation == other.Pronunciation && AccentType == other.AccentType &&
                   MoraCount == other.MoraCount && AccentAssociativeRule == other.AccentAssociativeRule;
        }


        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class UserDictWord {\n");
            sb.Append("  Surface: ").Append(Surface).Append("\n");
            sb.Append("  Priority: ").Append(Priority).Append("\n");
            sb.Append("  ContextId: ").Append(ContextId).Append("\n");
            sb.Append("  PartOfSpeech: ").Append(PartOfSpeech).Append("\n");
            sb.Append("  PartOfSpeechDetail1: ").Append(PartOfSpeechDetail1).Append("\n");
            sb.Append("  PartOfSpeechDetail2: ").Append(PartOfSpeechDetail2).Append("\n");
            sb.Append("  PartOfSpeechDetail3: ").Append(PartOfSpeechDetail3).Append("\n");
            sb.Append("  InflectionalType: ").Append(InflectionalType).Append("\n");
            sb.Append("  InflectionalForm: ").Append(InflectionalForm).Append("\n");
            sb.Append("  Stem: ").Append(Stem).Append("\n");
            sb.Append("  Yomi: ").Append(Yomi).Append("\n");
            sb.Append("  Pronunciation: ").Append(Pronunciation).Append("\n");
            sb.Append("  AccentType: ").Append(AccentType).Append("\n");
            sb.Append("  MoraCount: ").Append(MoraCount).Append("\n");
            sb.Append("  AccentAssociativeRule: ").Append(AccentAssociativeRule).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }


        public override bool Equals(object? obj)
        {
            return ReferenceEquals(this, obj) || (obj is UserDictWord other && Equals(other));
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Surface.GetHashCode();
                hashCode = (hashCode * 397) ^ Priority;
                hashCode = (hashCode * 397) ^ ContextId;
                hashCode = (hashCode * 397) ^ PartOfSpeech.GetHashCode();
                hashCode = (hashCode * 397) ^ PartOfSpeechDetail1.GetHashCode();
                hashCode = (hashCode * 397) ^ PartOfSpeechDetail2.GetHashCode();
                hashCode = (hashCode * 397) ^ PartOfSpeechDetail3.GetHashCode();
                hashCode = (hashCode * 397) ^ InflectionalType.GetHashCode();
                hashCode = (hashCode * 397) ^ InflectionalForm.GetHashCode();
                hashCode = (hashCode * 397) ^ Stem.GetHashCode();
                hashCode = (hashCode * 397) ^ Yomi.GetHashCode();
                hashCode = (hashCode * 397) ^ Pronunciation.GetHashCode();
                hashCode = (hashCode * 397) ^ AccentType;
                hashCode = (hashCode * 397) ^ MoraCount.GetHashCode();
                hashCode = (hashCode * 397) ^ AccentAssociativeRule.GetHashCode();
                return hashCode;
            }
        }
    }
}