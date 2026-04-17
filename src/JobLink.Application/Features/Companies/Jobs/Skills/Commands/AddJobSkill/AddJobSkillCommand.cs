using JobLink.Domain.Common.Results;
using MediatR;

namespace JobLink.Application.Features.Companies.Jobs.Skills.Commands.AddJobSkill;

public sealed record AddJobSkillCommand(
    Guid JobId,
    Guid SkillId,
    bool IsRequired
) : IRequest<Result<Guid>>;
