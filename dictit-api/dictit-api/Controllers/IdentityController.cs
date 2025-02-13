using DictItApi.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DictItApi.Controllers;

[Route("identity")]
[ApiController]
[Authorize]
public class IdentityController : ControllerBase
{
    private readonly SignInManager<User> _signInManager;

    public IdentityController(SignInManager<User> signInManager)
    {
        _signInManager = signInManager;
    }

    [HttpGet("check-auth")]
    public async Task<ActionResult> CheckAuth()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
        {
            return Unauthorized();
        }

        return Ok();
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok();
    }
}
