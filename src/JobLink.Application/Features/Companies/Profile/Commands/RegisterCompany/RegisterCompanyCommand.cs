using JobLink.Domain.Common.Results;
using MediatR;
using JobLink.Application.Common.DTOs;

namespace JobLink.Application.Features.Companies.Profile.Commands.RegisterCompany;

public sealed record RegisterCompanyCommand(
    RegisterUserDto User,
    string Name,
    string Industry
) : IRequest<Result<TokenDto>>;
