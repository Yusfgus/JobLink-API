using JobLink.Domain.Common.Results;
using MediatR;

namespace JobLink.Application.Features.JobSeekers.Pictures.Commands.UploadMyPicture;

public record UploadMyPictureCommand(
    Stream FileStream,
    string FileName,
    string ContentType
) : IRequest<Result<string>>;