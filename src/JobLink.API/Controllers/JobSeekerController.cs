using JobLink.API.Contracts.JobSeekers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using JobLink.API.Mappings;
using JobLink.Application.Features.JobSeekers.Queries.GetMyJobSeeker;
using Microsoft.AspNetCore.Authorization;
using JobLink.Domain.Common.Enums;

namespace JobLink.API.Controllers;

[ApiController]
[Route("api/v1/job-seekers")]
[Authorize(Roles = nameof(UserRole.JobSeeker))]
public class JobSeekerController(ISender sender) : ApiController
{
    [HttpGet("me")]
    public async Task<IActionResult> GetJobSeeker(CancellationToken ct)
    {
        var result = await sender.Send(new GetMyJobSeekerQuery(), ct);

        return result.Match(
            jobSeeker => Ok(jobSeeker),
            errors => Problem(errors)
        );
    }
}
