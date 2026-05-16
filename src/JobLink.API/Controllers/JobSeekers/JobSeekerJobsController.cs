using JobLink.API.Contracts;
using JobLink.Application.Features.JobSeekers.JobApplications.Queries.GetMyApplications;
using JobLink.Application.Features.JobSeekers.SavedJobs.Queries.GetMySavedJobs;
using JobLink.Domain.Common.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobLink.API.Controllers.JobSeekers;

[ApiController]
[Route("api/v1/job-seekers/me/jobs")]
[Authorize(Roles = nameof(UserRole.JobSeeker))]
public class JobSeekerJobsController(ISender sender) : ApiController
{
    [HttpGet("applied")]
    public async Task<IActionResult> GetMyApplications([FromQuery] PageRequest pageRequest, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetMyApplicationsQuery(pageRequest.Page, pageRequest.PageSize), cancellationToken);

        return result.Match(
            paginatedApplications => Ok(paginatedApplications),
            errors => Problem(errors)
        );
    }

    [HttpGet("applications/{id:guid}")]
    public Task<IActionResult> GetMyApplicationById(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();

        // var result = await sender.Send(new GetMyApplicationByIdQuery(id), cancellationToken);

        // return result.Match(
        //     applicationDetails => Ok(applicationDetails),
        //     errors => Problem(errors)
        // );
    }

    // [HttpDelete("applications/{id:guid}")]
    // public async Task<IActionResult> WithdrawApplication(Guid id, CancellationToken cancellationToken)
    // {
    //     var result = await sender.Send(new WithdrawApplicationCommand(id), cancellationToken);

    //     return result.Match(
    //         () => Ok("Job application withdrawn"),
    //         errors => Problem(errors)
    //     );
    // }

    [HttpGet("saved")]
    public async Task<IActionResult> GetMySavedJobs([FromQuery] PageRequest pageRequest, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetMySavedJobsQuery(pageRequest.Page, pageRequest.PageSize), cancellationToken);

        return result.Match(
            paginatedSavedJobs => Ok(paginatedSavedJobs),
            errors => Problem(errors)
        );
    }

    // [HttpDelete("saved/{id:guid}")]
    // public async Task<IActionResult> UnsaveJob(Guid id, CancellationToken cancellationToken)
    // {
    //     var result = await sender.Send(new UnsaveJobCommand(id), cancellationToken);

    //     return result.Match(
    //         () => Ok("Job removed from saved jobs"),
    //         errors => Problem(errors)
    //     );
    // }
}
