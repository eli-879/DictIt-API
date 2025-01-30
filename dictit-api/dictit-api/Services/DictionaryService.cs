using DictItApi.Dtos;
using DictItApi.Entities;
using DictItApi.Result;
using Microsoft.Extensions.Options;
using System.Net;
using System.Text.Json;

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

    public async Task<Result<DictionaryAPIResponseDto>> GetWordDefinitionAsync(string word)
    {
        var apiUrl = $"{_settings.DictionaryApiBase}/{word}";

        try
        {
            var response = await _httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var entry = JsonSerializer.Deserialize<List<DictionaryEntry>>(content);
            var returnObject = new DictionaryAPIResponseDto()
            {
                Entries = entry.ToArray()
            };

            return Result<DictionaryAPIResponseDto>.Success(returnObject);

        }
        catch (HttpRequestException ex) 
        {
            return Result<DictionaryAPIResponseDto>.Failure(ex.StatusCode ?? HttpStatusCode.InternalServerError, ex.Message);
        }
    }
}
