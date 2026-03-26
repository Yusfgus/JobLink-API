using JobLink.Domain.Common.Results;
using MediatR;

namespace JobLink.Application.Features.JobSeekers.Skills.Commands.DeleteJobSeekerSkill;

public sealed record DeleteJobSeekerSkillCommand(
    Guid Id
) : IRequest<Result>;
