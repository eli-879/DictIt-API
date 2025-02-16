using DictItApi.Dtos;
using DictItApi.Result;

namespace DictItApi.Services;

public interface ISaveWordService
{
    public Task<Result<SavedWordResponseDto>> GetSavedWordsByUser(string userId, string filter, string sort, string order, int page, int numPerPage);

    public Task<Result<SavedWordResponseDto>> SaveWordAsync(string word, string userId);

    public Task<Result<bool>> RemoveWordAsync(string word, string userId);
}
