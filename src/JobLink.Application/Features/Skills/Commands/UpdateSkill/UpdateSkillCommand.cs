using MediatR;
using JobLink.Domain.Common.Results;

namespace JobLink.Application.Features.Skills.Commands.UpdateSkill;

public sealed record UpdateSkillCommand(Guid Id, string Name) : IRequest<Result>;