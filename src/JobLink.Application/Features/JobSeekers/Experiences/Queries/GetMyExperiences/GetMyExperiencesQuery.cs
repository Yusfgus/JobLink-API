using JobLink.Application.Features.JobSeekers.DTOs;
using JobLink.Domain.Common.Results;
using MediatR;

namespace JobLink.Application.Features.JobSeekers.Experiences.Queries.GetMyExperiences;

public sealed record GetMyExperiencesQuery() : IRequest<Result<IEnumerable<ExperienceDto>>>;
