using JobLink.Domain.Common.Results;
using MediatR;

namespace JobLink.Application.Features.JobSeekers.Experiences.Commands.DeleteExperience;

public sealed record DeleteExperienceCommand(Guid Id) : IRequest<Result>;
