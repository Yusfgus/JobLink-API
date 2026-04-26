using JobLink.API.Contracts;
using JobLink.Application.Features.Jobs.Queries.GetJobs;
using JobLink.Application.Features.Jobs.Queries.GetJobById;
using JobLink.Application.Features.JobSeekers.JobApplications.Commands.ApplyForJob;
using JobLink.Application.Features.JobSeekers.JobApplications.Commands.WithdrawApplication;
using JobLink.Application.Features.JobSeekers.SavedJobs.Commands.SaveJob;
using JobLink.Application.Features.JobSeekers.SavedJobs.Commands.UnsaveJob;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using JobLink.Domain.Common.Enums;

namespace JobLink.API.Controllers;

[ApiController]
[Route("api/v1/jobs")]
[Authorize]
public class JobController(ISender sender) : ApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] PageRequest pageRequest, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetJobsQuery(pageRequest.Page, pageRequest.PageSize), cancellationToken);

        return result.Match(
            paginatedJobs => Ok(paginatedJobs),
            errors => Problem(errors)
        );
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetJobDetails(Guid id, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetJobByIdQuery(id), cancellationToken);

        return result.Match(
            jobDetails => Ok(jobDetails),
            errors => Problem(errors)
        );
    }

    [HttpPost("{id}/apply")]
    [Authorize(Roles = nameof(UserRole.JobSeeker))]
    public async Task<IActionResult> ApplyForJob(Guid id, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new ApplyForJobCommand(id), cancellationToken);

        return result.Match(
            NoContent,
            errors => Problem(errors)
        );
    }

    [HttpDelete("applications/{id:guid}")]
    [Authorize(Roles = nameof(UserRole.JobSeeker))]
    public async Task<IActionResult> WithdrawApplication(Guid id, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new WithdrawApplicationCommand(id), cancellationToken);

        return result.Match(
            NoContent,
            errors => Problem(errors)
        );
    }

    [HttpPost("{id}/save")]
    [Authorize(Roles = nameof(UserRole.JobSeeker))]
    public async Task<IActionResult> SaveJob(Guid id, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new SaveJobCommand(id), cancellationToken);

        return result.Match(
            NoContent,
            errors => Problem(errors)
        );
    }

    [HttpDelete("{id:guid}/unsave")]
    [Authorize(Roles = nameof(UserRole.JobSeeker))]
    public async Task<IActionResult> UnsaveJob(Guid id, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new UnsaveJobCommand(id), cancellationToken);

        return result.Match(
            NoContent,
            errors => Problem(errors)
        );
    }
}