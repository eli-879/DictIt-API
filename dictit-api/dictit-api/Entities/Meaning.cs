using System.Text.Json.Serialization;

namespace DictItApi.Entities;

public class Meaning
{
    [JsonPropertyName("partOfSpeech")]
    public string PartOfSpeech { get; set; } = string.Empty;

    [JsonPropertyName("definitions")]
    public Definition[] Definitions { get; set; } = [];
}
