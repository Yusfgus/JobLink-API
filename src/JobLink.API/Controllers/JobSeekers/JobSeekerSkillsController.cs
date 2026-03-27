using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using JobLink.Domain.Common.Enums;
using JobLink.API.Contracts.JobSeekers;

using JobLink.Application.Features.JobSeekers.Skills.Queries.GetMyJobSeekerSkills;
using JobLink.Application.Features.JobSeekers.Skills.Queries.GetMyJobSeekerSkillById;
using JobLink.Application.Features.JobSeekers.Skills.Commands.DeleteJobSeekerSkill;

namespace JobLink.API.Controllers.JobSeekers;

[ApiController]
[Route("api/v1/job-seekers/me/skills")]
[Authorize(Roles = nameof(UserRole.JobSeeker))]
public class JobSeekerSkillsController(ISender sender) : ApiController
{
    [HttpGet]
    public async Task<IActionResult> GetMyJobSeekerSkills(CancellationToken ct)
    {
        var result = await sender.Send(new GetMyJobSeekerSkillsQuery(), ct);

        return result.Match(
            skill => Ok(skill),
            errors => Problem(errors)
        );
    }

    [HttpGet("{jobSeekerSkillId:guid}")]
    public async Task<IActionResult> GetJobSeekerSkill(Guid jobSeekerSkillId, CancellationToken ct)
    {
        var result = await sender.Send(new GetMyJobSeekerSkillByIdQuery(jobSeekerSkillId), ct);

        return result.Match(
            skills => Ok(skills),
            errors => Problem(errors)
        );
    }

    [HttpPost]
    public async Task<IActionResult> AddJobSeekerSkill([FromBody] AddJobSeekerSkillRequest request, CancellationToken ct)
    {
        var result = await sender.Send(request.ToCommand(), ct);

        return result.Match(
            id => CreatedAtAction(nameof(GetJobSeekerSkill), new { jobSeekerSkillId = id }, id),
            errors => Problem(errors)
        );
    }

    [HttpPut("{jobSeekerSkillId:guid}")]
    public async Task<IActionResult> UpdateJobSeekerSkill(Guid jobSeekerSkillId, [FromBody] UpdateJobSeekerSkillRequest request, CancellationToken ct)
    {
        var result = await sender.Send(request.ToCommand(jobSeekerSkillId), ct);

        return result.Match(
            NoContent,
            errors => Problem(errors)
        );
    }

    [HttpDelete("{jobSeekerSkillId:guid}")]
    public async Task<IActionResult> DeleteJobSeekerSkill(Guid jobSeekerSkillId, CancellationToken ct)
    {
        var result = await sender.Send(new DeleteJobSeekerSkillCommand(jobSeekerSkillId), ct);

        return result.Match(
            NoContent,
            errors => Problem(errors)
        );
    }
}
