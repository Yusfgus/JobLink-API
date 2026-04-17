using JobLink.Domain.Common.Results;
using MediatR;

namespace JobLink.Application.Features.Companies.Jobs.Skills.Commands.UpdateJobSkill;

public sealed record UpdateJobSkillCommand(
    Guid JobId,
    Guid Id,
    Guid? SkillId,
    bool? IsRequired
) : IRequest<Result>;
