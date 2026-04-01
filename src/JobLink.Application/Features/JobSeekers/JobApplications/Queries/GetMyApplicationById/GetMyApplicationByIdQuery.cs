using JobLink.Application.Features.JobSeekers.DTOs;
using JobLink.Domain.Common.Results;
using MediatR;

namespace JobLink.Application.Features.JobSeekers.JobApplications.Queries.GetMyApplicationById;

public sealed record GetMyApplicationByIdQuery(Guid Id) : IRequest<Result<JobApplicationDetailsDto>>;
