using JobLink.API.Contracts.JobSeekers;
using JobLink.Application.Features.JobSeekers.Profile.Queries.GetJobSeekerById;
using JobLink.Application.Features.JobSeekers.Profile.Queries.GetMyJobSeeker;
using JobLink.Domain.Common.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobLink.API.Controllers.JobSeekers;

[ApiController]
[Route("api/v1/job-seekers")]
public class JobSeekerController(ISender sender) : ApiController
{
    [HttpGet("{id:guid}")]
    [Authorize(Roles = nameof(UserRole.Company) + "," + nameof(UserRole.Admin))]
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

    [HttpPut("me")]
    [Authorize(Roles = nameof(UserRole.JobSeeker))]
    public async Task<IActionResult> UpdateMyJobSeeker(UpdateJobSeekerRequest request, CancellationToken ct)
    {
        var result = await sender.Send(request.ToCommand(), ct);

        return result.Match(
            NoContent,
            errors => Problem(errors)
        );
    }
}
