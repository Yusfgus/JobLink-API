using JobLink.API.Contracts.JobSeekers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using JobLink.API.Mappings;

namespace JobLink.API.Controllers;

[ApiController]
[Route("api/job-seekers")]
public class JobSeekerController(ISender sender) : ApiController
{
    [HttpPost]
    public async Task<IActionResult> RegisterJobSeeker([FromBody] RegisterJobSeekerRequest request, CancellationToken ct)
    {
        var result = await sender.Send(request.ToCommand(), ct);

        return result.Match(
            id => CreatedAtAction(nameof(GetJobSeeker), new { id }, id),
            errors => Problem(errors)
        );
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetJobSeeker(Guid id, CancellationToken ct)
    {
        return Ok();
    }
}
