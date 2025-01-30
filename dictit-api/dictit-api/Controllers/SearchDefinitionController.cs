using DictItApi.Entities;
using DictItApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DictItApi.Controllers;

[Route("search")]
[ApiController]
public class SearchDefinitionController : ControllerBase
{
    private readonly IDictionaryService _dictionaryService;
    public SearchDefinitionController(IDictionaryService dictionaryService)
    {
        _dictionaryService = dictionaryService;
    }

    // GET search/hello
    [HttpGet("{word}")]
    public async Task<ActionResult<DictionaryEntry>> Get(string word)
    {
        
        var wordDefinitionResult = await _dictionaryService.GetWordDefinitionAsync(word);
        if (!wordDefinitionResult.IsSuccess)
        {
            return NotFound($"Couldn't find word: {word}");
        }

        return Ok(wordDefinitionResult.Value);
    }
}
