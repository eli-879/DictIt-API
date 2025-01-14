namespace DictItApi.Services
{
    public interface IDictionaryService
    {
        Task<string> GetWordDefinitionAsync(string word);
    }
}
