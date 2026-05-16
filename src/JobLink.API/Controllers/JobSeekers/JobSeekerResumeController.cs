using JobLink.Application.Features.JobSeekers.DTOs;
using JobLink.Application.Features.JobSeekers.Resumes.Commands.DeleteMyResume;
using JobLink.Application.Features.JobSeekers.Resumes.Commands.UploadMyResume;
using JobLink.Application.Features.JobSeekers.Resumes.Queries.GetMyResume;
using JobLink.Domain.Common.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobLink.API.Controllers.JobSeekers;

[ApiController]
[Route("api/v1/job-seekers/me/resume")]
[Authorize(Roles = nameof(UserRole.JobSeeker))]
public class JobSeekerResumeController(ISender sender) : ApiController
{
    [HttpGet]
    public async Task<IActionResult> GetMyResume(CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetMyResumeQuery(), cancellationToken);

        return result.Match(
            resume =>
            {
                resume.ResumeUrl = $"{Request.Scheme}://{Request.Host}/{resume.ResumeUrl}";
                return Ok(resume);
            },
            errors => Problem(errors)
        );
    }

    [HttpPost]
    public async Task<IActionResult> UploadMyResume(IFormFile resume, CancellationToken cancellationToken)
    {
        using Stream stream = resume.OpenReadStream();

        var command = new UploadMyResumeCommand(
            stream,
            resume.FileName,
            resume.ContentType);

        var result = await sender.Send(command, cancellationToken);

        return result.Match(
            resume =>
            {
                resume.ResumeUrl = $"{Request.Scheme}://{Request.Host}/{resume.ResumeUrl}";
                return Ok(resume);
            },
            errors => Problem(errors)
        );
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteMyResume(CancellationToken cancellationToken)
    {
        var result = await sender.Send(new DeleteMyResumeCommand(), cancellationToken);

        return result.Match(
            NoContent,
            errors => Problem(errors)
        );
    }
}