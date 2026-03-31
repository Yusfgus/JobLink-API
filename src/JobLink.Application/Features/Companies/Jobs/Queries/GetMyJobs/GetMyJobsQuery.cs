using JobLink.Application.Features.Jobs.Dtos;
using JobLink.Domain.Common.Results;
using MediatR;

namespace JobLink.Application.Features.Companies.Jobs.Queries.GetMyJobs;

public sealed record GetMyJobsQuery : IRequest<Result<IEnumerable<JobDto>>>;
