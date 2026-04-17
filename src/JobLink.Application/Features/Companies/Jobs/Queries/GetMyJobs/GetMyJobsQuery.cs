using JobLink.Application.Common.Models;
using JobLink.Application.Features.Companies.DTOs;
using JobLink.Domain.Common.Results;
using MediatR;

namespace JobLink.Application.Features.Companies.Jobs.Queries.GetMyJobs;

public sealed record GetMyJobsQuery(int Page = 1, int PageSize = 10) : IRequest<Result<PaginatedList<CompanyJobSummaryDto>>>;
