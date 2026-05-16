using JobLink.Application.Features.JobSeekers.Pictures.Commands.DeleteMyPicture;
using JobLink.Application.Features.JobSeekers.Pictures.Commands.UploadMyPicture;
using JobLink.Application.Features.JobSeekers.Pictures.Queries.GetMyPicture;
using JobLink.Domain.Common.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobLink.API.Controllers.JobSeekers;

[ApiController]
[Route("api/v1/job-seekers/me/picture")]
[Authorize(Roles = nameof(UserRole.JobSeeker))]
public class JobSeekerPictureController(ISender sender) : ApiController
{
    [HttpGet]
    public async Task<IActionResult> GetMyPicture(CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetMyPictureQuery(), cancellationToken);

        return result.Match(
            profilePictureUrl =>
                Ok(new
                {
                    profilePictureUrl = !string.IsNullOrEmpty(profilePictureUrl)
                        ? $"{Request.Scheme}://{Request.Host}/{profilePictureUrl}"
                        : null
                }),
            errors => Problem(errors)
        );
    }

    [HttpPost]
    public async Task<IActionResult> UploadMyPicture(IFormFile profilePicture, CancellationToken cancellationToken)
    {
        using Stream stream = profilePicture.OpenReadStream();

        var command = new UploadMyPictureCommand(
            stream,
            profilePicture.FileName,
            profilePicture.ContentType);

        var result = await sender.Send(command, cancellationToken);

        return result.Match(
            profilePictureUrl => Ok(new { profilePictureUrl = $"{Request.Scheme}://{Request.Host}/{profilePictureUrl}" }),
            errors => Problem(errors)
        );
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteMyPicture(CancellationToken cancellationToken)
    {
        var result = await sender.Send(new DeleteMyPictureCommand(), cancellationToken);

        return result.Match(
            NoContent,
            errors => Problem(errors)
        );
    }
}