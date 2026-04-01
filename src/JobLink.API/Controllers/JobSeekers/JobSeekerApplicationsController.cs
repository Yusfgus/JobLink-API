using JobLink.API.Contracts;
using JobLink.Application.Features.JobSeekers.JobApplications.Commands.WithdrawApplication;
using JobLink.Application.Features.JobSeekers.JobApplications.Queries.GetMyApplicationById;
using JobLink.Application.Features.JobSeekers.JobApplications.Queries.GetMyApplications;
using JobLink.Domain.Common.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobLink.API.Controllers.JobSeekers;

[ApiController]
[Route("api/v1/job-seekers/me/applications")]
[Authorize(Roles = nameof(UserRole.JobSeeker))]
public class JobSeekerApplicationsController(ISender sender) : ApiController
{
    [HttpGet]
    public async Task<IActionResult> GetMyApplications([FromQuery] PageRequest pageRequest, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetMyApplicationsQuery(pageRequest.Page, pageRequest.PageSize), cancellationToken);

        return result.Match(
            paginatedApplications => Ok(paginatedApplications),
            errors => Problem(errors)
        );
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetMyApplicationById(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();

        var result = await sender.Send(new GetMyApplicationByIdQuery(id), cancellationToken);

        return result.Match(
            applicationDetails => Ok(applicationDetails),
            errors => Problem(errors)
        );
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> WithdrawApplication(Guid id, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new WithdrawApplicationCommand(id), cancellationToken);

        return result.Match(
            () => Ok("Job application withdrawn"),
            errors => Problem(errors)
        );
    }
}
