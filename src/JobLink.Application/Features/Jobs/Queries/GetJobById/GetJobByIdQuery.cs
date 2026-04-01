using JobLink.Application.Features.Jobs.DTOs;
using JobLink.Domain.Common.Results;
using MediatR;

namespace JobLink.Application.Features.Jobs.Queries.GetJobById;

public sealed record GetJobByIdQuery(Guid Id) : IRequest<Result<JobDetailsDto>>;
