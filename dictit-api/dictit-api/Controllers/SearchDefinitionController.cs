using DictItApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

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
    public async Task<IActionResult> Get(string word)
    {
        var wordDefinition = await _dictionaryService.GetWordDefinitionAsync(word);
        return Ok(wordDefinition);
    }
}
