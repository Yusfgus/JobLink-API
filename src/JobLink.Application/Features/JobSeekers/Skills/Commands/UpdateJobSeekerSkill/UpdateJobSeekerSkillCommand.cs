using JobLink.Domain.Common.Enums;
using JobLink.Domain.Common.Results;
using MediatR;

namespace JobLink.Application.Features.JobSeekers.Skills.Commands.UpdateJobSeekerSkill;

public sealed record UpdateJobSeekerSkillCommand(
    Guid JobSeekerSkillId,
    Guid SkillId,
    SkillLevel SkillLevel
) : IRequest<Result>;
