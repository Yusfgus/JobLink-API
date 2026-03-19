using JobLink.Domain.Common.Results;
using MediatR;
using JobLink.Application.Common.DTOs;

namespace JobLink.Application.Features.Identity.Queries.LogIn;

public sealed record LogInQuery(
    string Email,
    string Password
) : IRequest<Result<TokenDto>>;
