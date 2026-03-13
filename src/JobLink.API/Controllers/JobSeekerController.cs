using JobLink.API.Contracts.JobSeekers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using JobLink.API.Mappings;
using JobLink.Application.Features.JobSeekers.Queries.GetJobSeekerById;

namespace JobLink.API.Controllers;

[ApiController]
[Route("api/v1/job-seekers")]
public class JobSeekerController(ISender sender) : ApiController
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetJobSeeker(Guid id, CancellationToken ct)
    {
        var result = await sender.Send(new GetJobSeekerByIdQuery(id), ct);

        return result.Match(
            jobSeeker => Ok(jobSeeker),
            errors => Problem(errors)
        );
    }
}
