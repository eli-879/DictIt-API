using DictItApi.Dtos;
using DictItApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DictItApi.Controllers
{
    [Route("saved-words")]
    [ApiController]
    [Authorize]
    public class SaveWordController : ControllerBase
    {
        private readonly ISaveWordService _savedWordService;

        public SaveWordController(ISaveWordService savedWordService)
        {
            _savedWordService = savedWordService;
        }

        [HttpGet("")]
        public async Task<ActionResult> GetAllSavedWords()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized();
            }

            var savedWordsResult = await _savedWordService.GetSavedWordsByUser(userId);

            if (!savedWordsResult.IsSuccess)
            {
                return BadRequest();
            }

            return Ok(savedWordsResult.Value);
        }

        [HttpPost("")]
        public async Task<ActionResult> SaveWord(SaveWordRequestDto saveWordDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized();
            }

            var saveWordResult = await _savedWordService.SaveWordAsync(saveWordDto.Word, userId);

            if (!saveWordResult.IsSuccess)
            {
                // TODO Different HTTP codes for different errors.
                return BadRequest();
            }
            return Ok();
        }

        [HttpDelete("")]
        public async Task<ActionResult> RemoveWord(SaveWordRequestDto removeWordDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized();
            }

            var removeWordResult = await _savedWordService.RemoveWordAsync(removeWordDto.Word, userId);

            if (!removeWordResult.IsSuccess)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
