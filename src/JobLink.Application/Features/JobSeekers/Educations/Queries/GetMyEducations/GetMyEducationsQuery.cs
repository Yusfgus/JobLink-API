using JobLink.Application.Features.JobSeekers.DTOs;
using JobLink.Domain.Common.Results;
using MediatR;

namespace JobLink.Application.Features.JobSeekers.Educations.Queries.GetMyEducations;

public sealed record GetMyEducationsQuery : IRequest<Result<IEnumerable<EducationDto>>>;
