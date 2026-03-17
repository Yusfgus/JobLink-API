using MediatR;
using JobLink.Domain.Common.Results;

namespace JobLink.Application.Features.Skills.Commands.CreateSkill;

public sealed record CreateSkillCommand(string Name) : IRequest<Result<Guid>>;