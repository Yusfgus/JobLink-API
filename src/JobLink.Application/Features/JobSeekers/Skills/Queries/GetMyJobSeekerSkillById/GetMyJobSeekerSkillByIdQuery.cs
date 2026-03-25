using JobLink.Application.Features.JobSeekers.DTOs;
using JobLink.Domain.Common.Results;
using MediatR;

namespace JobLink.Application.Features.JobSeekers.Skills.Queries.GetMyJobSeekerSkillById;

public sealed record GetMyJobSeekerSkillByIdQuery(Guid JobSeekerSkillId) : IRequest<Result<JobSeekerSkillDto>>;