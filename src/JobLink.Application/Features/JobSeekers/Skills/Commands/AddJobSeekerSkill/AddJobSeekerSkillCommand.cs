using JobLink.Domain.Common.Enums;
using JobLink.Domain.Common.Results;
using MediatR;

namespace JobLink.Application.Features.JobSeekers.Skills.Commands.AddJobSeekerSkill;

public sealed record AddJobSeekerSkillCommand(
    Guid SkillId,
    SkillLevel SkillLevel
) : IRequest<Result<Guid>>;
