using DictItApi.Result;

namespace DictItApi.Services;

public interface ISaveWordService
{
    public Task<Result<bool>> SaveWordAsync(string word, string userId);
}
