using JobLink.Application.Common.Models;
using JobLink.Application.Features.Companies.DTOs;
using JobLink.Domain.Common.Results;
using MediatR;

namespace JobLink.Application.Features.Companies.Jobs.Queries.GetJobApplicants;

public sealed record GetJobApplicantsQuery(Guid JobId, int Page, int PageSize) : IRequest<Result<PaginatedList<JobApplicantsDto>>>;
