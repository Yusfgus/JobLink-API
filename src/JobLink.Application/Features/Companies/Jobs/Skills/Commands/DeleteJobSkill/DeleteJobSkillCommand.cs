using JobLink.Domain.Common.Results;
using MediatR;

namespace JobLink.Application.Features.Companies.Jobs.Skills.Commands.DeleteJobSkill;

public sealed record DeleteJobSkillCommand(Guid JobId, Guid Id) : IRequest<Result>;
