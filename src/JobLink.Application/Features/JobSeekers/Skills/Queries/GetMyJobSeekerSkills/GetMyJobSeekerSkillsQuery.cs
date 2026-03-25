using JobLink.Application.Features.JobSeekers.DTOs;
using JobLink.Domain.Common.Results;
using MediatR;

namespace JobLink.Application.Features.JobSeekers.Skills.Queries.GetMyJobSeekerSkills;

public sealed record GetMyJobSeekerSkillsQuery : IRequest<Result<IEnumerable<JobSeekerSkillDto>>>;
