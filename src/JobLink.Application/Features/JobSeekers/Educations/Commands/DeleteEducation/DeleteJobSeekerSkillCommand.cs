using JobLink.Domain.Common.Results;
using MediatR;

namespace JobLink.Application.Features.JobSeekers.Educations.Commands.DeleteEducation;

public sealed record DeleteEducationCommand(
    Guid Id
) : IRequest<Result>;
