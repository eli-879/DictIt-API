using Microsoft.Extensions.Options;

namespace DictItApi.Services;

public class DictionaryService : IDictionaryService
{
    private readonly HttpClient _httpClient;
    private readonly ApiSettings _settings;

    public DictionaryService(HttpClient httpClient, IOptions<ApiSettings> apiSettings) 
    {
        _httpClient = httpClient;
        _settings = apiSettings.Value;
    }

    public async Task<string> GetWordDefinitionAsync(string word)
    {
        var apiUrl = $"{_settings.DictionaryApiBase}/{word}";

        var response = await _httpClient.GetAsync(apiUrl);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();

    }
}
