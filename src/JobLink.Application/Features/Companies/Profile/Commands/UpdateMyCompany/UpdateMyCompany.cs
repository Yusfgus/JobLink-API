using JobLink.Domain.Common.Results;
using MediatR;

namespace JobLink.Application.Features.Companies.Profile.Commands.UpdateMyCompany;

public sealed record UpdateMyCompanyCommand(
    string? Name,
    string? Industry,
    string? Description,
    string? LogoUrl,
    string? Website
) : IRequest<Result>;
