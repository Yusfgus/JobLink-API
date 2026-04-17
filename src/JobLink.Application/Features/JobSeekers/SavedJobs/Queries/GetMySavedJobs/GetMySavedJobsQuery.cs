using JobLink.Application.Common.Models;
using JobLink.Application.Features.JobSeekers.DTOs;
using JobLink.Domain.Common.Results;
using MediatR;

namespace JobLink.Application.Features.JobSeekers.SavedJobs.Queries.GetMySavedJobs;

public sealed record GetMySavedJobsQuery(int Page = 1, int PageSize = 10) : IRequest<Result<PaginatedList<SavedJobDto>>>;
