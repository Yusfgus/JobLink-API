using JobLink.Domain.Common.Results;
using JobLink.Application.Features.Skills.DTOs;
using MediatR;

namespace JobLink.Application.Features.Skills.Queries.GetSkills;

public sealed record GetSkillsQuery : IRequest<Result<List<SkillDto>>>;
