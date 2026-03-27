using JobLink.API.Contracts.JobSeekers;

using JobLink.Application.Features.JobSeekers.Experiences.Commands.DeleteExperience;
using JobLink.Application.Features.JobSeekers.Experiences.Queries.GetMyExperienceById;
using JobLink.Application.Features.JobSeekers.Experiences.Queries.GetMyExperiences;
using JobLink.Domain.Common.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobLink.API.Controllers.JobSeekers;

[ApiController]
[Route("api/v1/job-seekers/me/experiences")]
[Authorize(Roles = nameof(UserRole.JobSeeker))]
public class ExperienceController(ISender sender) : ApiController
{
    [HttpGet]
    public async Task<IActionResult> GetExperiences(CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetMyExperiencesQuery(), cancellationToken);

        return result.Match(
            success => Ok(success),
            errors => Problem(errors));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetExperienceById(Guid id, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetMyExperienceByIdQuery(id), cancellationToken);

        return result.Match(
            success => Ok(success),
            errors => Problem(errors));
    }

    [HttpPost]
    public async Task<IActionResult> AddExperience([FromBody] AddExperienceRequest request, CancellationToken cancellationToken)
    {
        var result = await sender.Send(request.ToCommand(), cancellationToken);

        return result.Match(
            id => CreatedAtAction(nameof(GetExperienceById), new { id }, id),
            errors => Problem(errors));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateExperience(Guid id, [FromBody] UpdateExperienceRequest request, CancellationToken cancellationToken)
    {
        var result = await sender.Send(request.ToCommand(id), cancellationToken);

        return result.Match(
            NoContent,
            errors => Problem(errors));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteExperience(Guid id, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new DeleteExperienceCommand(id), cancellationToken);

        return result.Match(
            NoContent,
            errors => Problem(errors));
    }
}
