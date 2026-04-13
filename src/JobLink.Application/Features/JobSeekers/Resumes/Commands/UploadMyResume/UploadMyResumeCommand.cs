using JobLink.Domain.Common.Results;
using MediatR;

namespace JobLink.Application.Features.JobSeekers.Resumes.Commands.UploadMyResume;

public record UploadMyResumeCommand(
    Stream FileStream,
    string FileName,
    string ContentType
) : IRequest<Result<Guid>>;