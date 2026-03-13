using JobLink.API.Contracts.Authentication;
using JobLink.API.Contracts.JobSeekers;
using JobLink.API.Contracts.Companies;
using JobLink.API.Mappings;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JobLink.API.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class AuthController(ISender sender) : ApiController
{
    [HttpPost("register/job-seeker")]
    public async Task<IActionResult> RegisterJobSeeker([FromBody] RegisterJobSeekerRequest request, CancellationToken ct)
    {
        var result = await sender.Send(request.ToCommand(), ct);

        return result.Match(
            id => CreatedAtAction(nameof(JobSeekerController.GetJobSeeker), "JobSeeker", new { id }, id),
            errors => Problem(errors)
        );
    }

    [HttpPost("register/company")]
    public async Task<IActionResult> RegisterCompany([FromBody] RegisterCompanyRequest request, CancellationToken ct)
    {
        var result = await sender.Send(request.ToCommand(), ct);

        return result.Match(
            id => CreatedAtAction(nameof(CompanyController.GetCompany), "Company", new { id }, id),
            errors => Problem(errors)
        );
    }

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