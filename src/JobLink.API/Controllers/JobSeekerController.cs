using JobLink.API.Contracts.JobSeekers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using JobLink.API.Mappings;
using JobLink.Application.Features.JobSeekers.Queries.GetJobSeekerById;

namespace JobLink.API.Controllers;

[ApiController]
[Route("api/job-seekers")]
public class JobSeekerController(ISender sender) : ApiController
{
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterJobSeekerRequest request, CancellationToken ct)
    {
        var result = await sender.Send(request.ToCommand(), ct);

        return result.Match(
            id => CreatedAtAction(nameof(GetJobSeeker), new { id }, id),
            errors => Problem(errors)
        );
    }

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
