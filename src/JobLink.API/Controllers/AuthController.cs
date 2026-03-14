using JobLink.API.Contracts.Authentication;
using JobLink.API.Contracts.JobSeekers;
using JobLink.API.Contracts.Companies;
using JobLink.API.Mappings;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace JobLink.API.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class AuthController(ISender sender) : ApiController
{
    [HttpPost("register/job-seeker")]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterJobSeeker([FromBody] RegisterJobSeekerRequest request, CancellationToken ct)
    {
        var result = await sender.Send(request.ToCommand(), ct);

        return result.Match(
            tokenDto => Ok(tokenDto),
            errors => Problem(errors)
        );
    }

    [HttpPost("register/company")]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterCompany([FromBody] RegisterCompanyRequest request, CancellationToken ct)
    {
        var result = await sender.Send(request.ToCommand(), ct);

        return result.Match(
            tokenDto => Ok(tokenDto),
            errors => Problem(errors)
        );
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public Task<IActionResult> LogIn([FromBody] LogInRequest request, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    [HttpPost("refresh-token")]
    [AllowAnonymous]
    public Task<IActionResult> RefreshToken(CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    [HttpPost("logout")]
    [Authorize]
    public Task<IActionResult> LogOut(CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    [HttpGet("me")]
    [Authorize]
    public IActionResult GetMe()
    {
        return Ok(new
        {
            Id = User.FindFirstValue(ClaimTypes.NameIdentifier),
            Email = User.FindFirstValue(ClaimTypes.Email),
            Role = User.FindFirstValue(ClaimTypes.Role)
        });
    }
}