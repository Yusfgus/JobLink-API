using JobLink.API.Contracts.Admin;
using JobLink.API.Contracts.Authentication;
using JobLink.API.Contracts.JobSeekers;
using JobLink.API.Contracts.Companies;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using JobLink.Domain.Common.Enums;
using JobLink.Application.Features.Identity.Commands.LogOut;
using JobLink.Application.Features.Identity.Commands.RefreshToken;
using JobLink.Application.Features.Identity.Queries.LogIn;

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

    [HttpPost("register/admin")]
    [Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<IActionResult> RegisterAdmin([FromBody] RegisterAdminRequest request, CancellationToken ct)
    {
        var result = await sender.Send(request.ToCommand(), ct);

        return result.Match(
            tokenDto => Ok(tokenDto),
            errors => Problem(errors)
        );
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> LogIn([FromBody] LogInRequest request, CancellationToken ct)
    {
        var result = await sender.Send(new LogInQuery(request.Email, request.Password), ct);

        return result.Match(
            tokenDto => Ok(tokenDto),
            errors => Problem(errors)
        );
    }

    [HttpPost("refresh")]
    [AllowAnonymous]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request, CancellationToken ct)
    {
        var result = await sender.Send(new RefreshTokenCommand(request.RefreshToken), ct);

        return result.Match(
            tokenDto => Ok(tokenDto),
            errors => Problem(errors)
        );
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> LogOut(CancellationToken ct)
    {
        var result = await sender.Send(new LogOutCommand(), ct);

        return result.Match(
            NoContent,
            errors => Problem(errors)
        );
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