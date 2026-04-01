using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using JobLink.Domain.Common.Enums;
using JobLink.API.Contracts.Skills;

using JobLink.Application.Features.Skills.Queries.GetSkills;
using JobLink.Application.Features.Skills.Queries.GetSkillById;

namespace JobLink.API.Controllers;

[ApiController]
[Route("api/v1/skills")]
[Authorize]
public class SkillController(ISender sender) : ApiController
{
    [HttpGet]
    public async Task<IActionResult> GetSkills(CancellationToken ct)
    {
        var result = await sender.Send(new GetSkillsQuery(), ct);

        return result.Match(
            skills => Ok(skills),
            error => Problem(error)
        );
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetSkillById(Guid id, CancellationToken ct)
    {
        var result = await sender.Send(new GetSkillByIdQuery(id), ct);

        return result.Match(
            skill => Ok(skill),
            error => Problem(error)
        );
    }

    [HttpPost]
    [Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<IActionResult> CreateSkill([FromBody] CreateSkillRequest request, CancellationToken ct)
    {
        var result = await sender.Send(request.ToCommand(), ct);

        return result.Match(
            id => CreatedAtAction(nameof(GetSkillById), new { id }, id),
            error => Problem(error)
        );
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateSkill(Guid id, [FromBody] UpdateSkillRequest request, CancellationToken ct)
    {
        var result = await sender.Send(request.ToCommand(id), ct);

        return result.Match(
            NoContent,
            error => Problem(error)
        );
    }

    [HttpDelete("{id:guid}")]
    public Task<IActionResult> DeleteSkill(Guid id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}