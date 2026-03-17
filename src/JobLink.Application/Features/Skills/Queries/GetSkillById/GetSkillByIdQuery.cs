using JobLink.Domain.Common.Results;
using JobLink.Application.Features.Skills.DTOs;
using MediatR;

namespace JobLink.Application.Features.Skills.Queries.GetSkillById;

public sealed record GetSkillByIdQuery(Guid Id) : IRequest<Result<SkillDto>>;
