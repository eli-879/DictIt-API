using System.Text.Json.Serialization;

namespace DictItApi.Entities;

public class Phonetic
{
    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;

    [JsonPropertyName("audio")]
    public string Audio {  get; set; } = string.Empty;
}
