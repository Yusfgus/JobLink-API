using JobLink.Application.Features.JobSeekers.DTOs;
using JobLink.Domain.Common.Results;
using MediatR;

namespace JobLink.Application.Features.JobSeekers.Educations.Queries.GetMyEducationById;

public sealed record GetMyEducationByIdQuery(Guid Id) : IRequest<Result<EducationDto>>;