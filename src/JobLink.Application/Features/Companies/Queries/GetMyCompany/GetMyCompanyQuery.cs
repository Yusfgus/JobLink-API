using JobLink.Application.Features.Companies.DTOs;
using JobLink.Domain.Common.Results;
using MediatR;

namespace JobLink.Application.Features.Companies.Queries.GetMyCompany;

public sealed record GetMyCompanyQuery() : IRequest<Result<CompanyProfileDto>>;