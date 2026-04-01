using JobLink.Application.Common.Models;
using JobLink.Application.Features.Jobs.DTOs;
using JobLink.Domain.Common.Results;
using MediatR;

namespace JobLink.Application.Features.Jobs.Queries.GetJobs;

public sealed record GetJobsQuery(int Page = 1, int PageSize = 10) : IRequest<Result<PaginatedList<JobSummaryDto>>>;
