using JobLink.Application.Features.Companies.DTOs;
using JobLink.Domain.Common.Results;
using MediatR;

namespace JobLink.Application.Features.Companies.Profile.Queries.GetCompanyById;

public sealed record GetCompanyByIdQuery(Guid Id) : IRequest<Result<CompanyProfileDto>>;