using System.Text.Json.Serialization;

namespace DictItApi.Entities;

public class DictionaryEntry
{
    [JsonPropertyName("word")]
    public string Word { get; set; } = string.Empty;

    [JsonPropertyName("phonetics")]
    public Phonetic[] Phonetics {  get; set; } = [];

    [JsonPropertyName("meanings")]
    public Meaning[] Meanings { get; set; } = [];
}
