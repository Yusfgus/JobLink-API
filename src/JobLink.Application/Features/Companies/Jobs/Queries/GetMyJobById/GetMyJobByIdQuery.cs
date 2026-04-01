using JobLink.Application.Features.Companies.DTOs;
using JobLink.Domain.Common.Results;
using MediatR;

namespace JobLink.Application.Features.Companies.Jobs.Queries.GetMyJobById;

public sealed record GetMyJobByIdQuery(Guid Id) : IRequest<Result<CompanyJobDto>>;
