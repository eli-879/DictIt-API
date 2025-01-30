using DictItApi.Dtos;
using DictItApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DictItApi.Controllers
{
    [Route("save-word")]
    [ApiController]
    public class SaveWordController : ControllerBase
    {
        private readonly ISaveWordService _savedWordService;

        public SaveWordController(ISaveWordService savedWordService)
        {
            _savedWordService = savedWordService;
        }

        [HttpPost("")]
        public async Task<ActionResult> SaveWord(SaveWordRequestDto savedWordDto)
        {
            var saveWordResult = await _savedWordService.SaveWordAsync(savedWordDto.Word, savedWordDto.UserId);

            if (!saveWordResult.IsSuccess)
            {
                // TODO Different HTTP codes for different errors.
                return BadRequest();
            }
            return Ok();
        }
    }
}
