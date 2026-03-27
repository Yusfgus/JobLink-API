using JobLink.Application.Features.JobSeekers.DTOs;
using JobLink.Domain.Common.Results;
using MediatR;

namespace JobLink.Application.Features.JobSeekers.Experiences.Queries.GetMyExperienceById;

public sealed record GetMyExperienceByIdQuery(Guid Id) : IRequest<Result<ExperienceDto>>;
