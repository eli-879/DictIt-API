using System.Text.Json.Serialization;

namespace DictItApi.Entities;

public class Definition
{
    [JsonPropertyName("definition")]
    public string DictionaryDefinition {  get; set; } = string.Empty;

    [JsonPropertyName("example")]
    public string Example {  get; set; } = string.Empty;
}
