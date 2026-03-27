using JobLink.API.Contracts.JobSeekers;

using JobLink.Application.Features.JobSeekers.Educations.Commands.DeleteEducation;
using JobLink.Application.Features.JobSeekers.Educations.Queries.GetMyEducationById;
using JobLink.Application.Features.JobSeekers.Educations.Queries.GetMyEducations;
using JobLink.Domain.Common.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobLink.API.Controllers.JobSeekers;

[ApiController]
[Route("api/v1/job-seekers/me/educations")]
[Authorize(Roles = nameof(UserRole.JobSeeker))]
public class EducationController(ISender sender) : ApiController
{
    [HttpGet]
    public async Task<IActionResult> GetEducations(CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetMyEducationsQuery(), cancellationToken);

        return result.Match(
            success => Ok(success),
            errors => Problem(errors));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetEducationById(Guid id, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetMyEducationByIdQuery(id), cancellationToken);

        return result.Match(
            success => Ok(success),
            errors => Problem(errors));
    }

    [HttpPost]
    public async Task<IActionResult> AddEducation([FromBody] AddEducationRequest request, CancellationToken cancellationToken)
    {
        var result = await sender.Send(request.ToCommand(), cancellationToken);

        return result.Match(
            id => CreatedAtAction(nameof(GetEducationById), new { id }, id),
            errors => Problem(errors));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateEducation(Guid id, [FromBody] UpdateEducationRequest request, CancellationToken cancellationToken)
    {
        var result = await sender.Send(request.ToCommand(id), cancellationToken);

        return result.Match(
            NoContent,
            errors => Problem(errors));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteEducation(Guid id, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new DeleteEducationCommand(id), cancellationToken);

        return result.Match(
            NoContent,
            errors => Problem(errors));
    }
}