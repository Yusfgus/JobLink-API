using JobLink.API.Contracts.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JobLink.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(ISender sender) : ApiController
{
    [HttpPost("login")]
    public async Task<IActionResult> LogIn([FromBody] LogInRequest request, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken(CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    [HttpPost("logout")]
    public async Task<IActionResult> LogOut(CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetMe(CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}