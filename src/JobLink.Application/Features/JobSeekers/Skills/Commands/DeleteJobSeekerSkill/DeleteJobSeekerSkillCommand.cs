using JobLink.Domain.Common.Results;
using MediatR;

namespace JobLink.Application.Features.JobSeekers.Skills.Commands.DeleteJobSeekerSkill;

public sealed record DeleteJobSeekerSkillCommand(
    Guid JobSeekerSkillId
) : IRequest<Result>;
