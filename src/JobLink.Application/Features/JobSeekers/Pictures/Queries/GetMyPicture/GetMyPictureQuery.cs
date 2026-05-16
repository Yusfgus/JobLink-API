using JobLink.Domain.Common.Results;
using MediatR;

namespace JobLink.Application.Features.JobSeekers.Pictures.Queries.GetMyPicture;

public record GetMyPictureQuery : IRequest<Result<string>>;
