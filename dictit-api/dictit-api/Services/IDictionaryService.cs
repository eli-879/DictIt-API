using DictItApi.Entities;
using DictItApi.Result;

namespace DictItApi.Services;

public interface IDictionaryService
{
    Task<Result<DictionaryAPIResponseDto>> GetWordDefinitionAsync(string word);
}
