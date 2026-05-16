using JobLink.Domain.Common.Results;
using MediatR;

namespace JobLink.Application.Features.JobSeekers.Pictures.Commands.DeleteMyPicture;

public record DeleteMyPictureCommand : IRequest<Result>;
