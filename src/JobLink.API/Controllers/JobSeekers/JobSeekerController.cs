using MediatR;
using Microsoft.AspNetCore.Mvc;
using JobLink.Application.Features.JobSeekers.Queries.GetMyJobSeeker;
using Microsoft.AspNetCore.Authorization;
using JobLink.Domain.Common.Enums;
using JobLink.Application.Features.JobSeekers.Queries.GetJobSeekerById;

namespace JobLink.API.Controllers.JobSeekers;

[ApiController]
[Route("api/v1/job-seekers")]
public class JobSeekerController(ISender sender) : ApiController
{
    [HttpGet("{id:guid}")]
    [Authorize(Roles = nameof(UserRole.Company))]
    public async Task<IActionResult> GetJobSeeker(Guid id, CancellationToken ct)
    {
        var result = await sender.Send(new GetJobSeekerByIdQuery(id), ct);

        return result.Match(
            jobSeeker => Ok(jobSeeker),
            errors => Problem(errors)
        );
    }

    [HttpGet("me")]
    [Authorize(Roles = nameof(UserRole.JobSeeker))]
    public async Task<IActionResult> GetMyJobSeeker(CancellationToken ct)
    {
        var result = await sender.Send(new GetMyJobSeekerQuery(), ct);

        return result.Match(
            jobSeeker => Ok(jobSeeker),
            errors => Problem(errors)
        );
    }
}
