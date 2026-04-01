using JobLink.API.Contracts;
using JobLink.API.Contracts.Companies;
using JobLink.Application.Features.Companies.Jobs.Commands.CloseJob;
using JobLink.Application.Features.Companies.Jobs.Queries.GetJobApplicants;
using JobLink.Application.Features.Companies.Jobs.Queries.GetMyJobById;
using JobLink.Application.Features.Companies.Jobs.Queries.GetMyJobs;
using JobLink.Domain.Common.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobLink.API.Controllers.Companies;

[ApiController]
[Route("api/v1/companies/me/jobs")]
[Authorize(Roles = nameof(UserRole.Company))]
public class CompanyJobController(ISender sender) : ApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAllMyJob([FromQuery] PageRequest request, CancellationToken ct)
    {
        var result = await sender.Send(new GetMyJobsQuery(request.Page, request.PageSize), ct);

        return result.Match(
            paginatedJobs => Ok(paginatedJobs),
            errors => Problem(errors)
        );
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMyJob(Guid id, CancellationToken ct)
    {
        var result = await sender.Send(new GetMyJobByIdQuery(id), ct);

        return result.Match(
            job => Ok(job),
            errors => Problem(errors)
        );
    }

    [HttpPost]
    public async Task<IActionResult> CreateJob([FromBody] CreateJobRequest request, CancellationToken ct)
    {
        var result = await sender.Send(request.ToCommand(), ct);

        return result.Match(
            id => CreatedAtAction(nameof(GetMyJob), new { id }, id),
            errors => Problem(errors)
        );
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateJob(Guid id, [FromBody] UpdateJobRequest request, CancellationToken ct)
    {
        var result = await sender.Send(request.ToCommand(id), ct);

        return result.Match(
            NoContent,
            errors => Problem(errors)
        );
    }

    [HttpPatch("{id}/close")]
    public async Task<IActionResult> CloseJob(Guid id, CancellationToken ct)
    {
        var result = await sender.Send(new CloseJobCommand(id), ct);

        return result.Match(
            NoContent,
            errors => Problem(errors)
        );
    }

    [HttpGet("{id:guid}/applications")]
    public async Task<IActionResult> GetJobApplications(Guid id, [FromQuery] PageRequest request, CancellationToken ct)
    {
        var result = await sender.Send(new GetJobApplicantsQuery(id, request.Page, request.PageSize), ct);

        return result.Match(
            paginatedApplications => Ok(paginatedApplications),
            errors => Problem(errors)
        );
    }
}