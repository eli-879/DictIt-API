using DictItApi.Dtos;
using DictItApi.Result;

namespace DictItApi.Services;

public interface ISaveWordService
{
    public Task<Result<List<SavedWordResponseDto>>> GetSavedWordsByUser(string userId);

    public Task<Result<bool>> SaveWordAsync(string word, string userId);

    public Task<Result<bool>> RemoveWordAsync(string word, string userId);
}
