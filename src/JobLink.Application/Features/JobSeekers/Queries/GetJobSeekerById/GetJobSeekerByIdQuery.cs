using JobLink.Application.Common.DTOs;
using JobLink.Domain.Common.Results;
using MediatR;

namespace JobLink.Application.Features.JobSeekers.Queries.GetJobSeekerById;

public sealed record GetJobSeekerByIdQuery(Guid Id) : IRequest<Result<JobSeekerDto>>;