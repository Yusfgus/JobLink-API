using JobLink.Application.Common.Models;
using JobLink.Application.Features.JobSeekers.DTOs;
using JobLink.Domain.Common.Results;
using MediatR;

namespace JobLink.Application.Features.JobSeekers.JobApplications.Queries.GetMyApplications;

public sealed record GetMyApplicationsQuery(int Page, int PageSize) : IRequest<Result<PaginatedList<JobApplicationSummaryDto>>>;
