using JobLink.Application.Features.JobSeekers.Resumes.Commands.DeleteMyResume;
using JobLink.Application.Features.JobSeekers.Resumes.Commands.UploadMyResume;
using JobLink.Application.Features.JobSeekers.Resumes.Queries.DownloadMyResume;
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
            resume => Ok(resume),
            errors => Problem(errors)
        );
    }

    [HttpGet("download")]
    public async Task<IActionResult> DownloadMyResume(CancellationToken cancellationToken)
    {
        var result = await sender.Send(new DownloadMyResumeQuery(), cancellationToken);

        return result.Match(
            stream => File(stream, "application/pdf", "resume.pdf"),
            errors => Problem(errors)
        );
    }

    [HttpPost]
    public async Task<IActionResult> UploadMyResume(IFormFile file, CancellationToken cancellationToken)
    {
        using Stream stream = file.OpenReadStream();

        var command = new UploadMyResumeCommand(
            stream,
            file.FileName,
            file.ContentType);

        var result = await sender.Send(command, cancellationToken);

        return result.Match(
            resumeId => Ok(resumeId),
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