using JobLink.Domain.Common.Results;
using MediatR;
using JobLink.Domain.Common.Enums;
using JobLink.Application.Common.DTOs;

namespace JobLink.Application.Features.Admins.Commands.RegisterAdmin;

public sealed record RegisterAdminCommand(
    RegisterUserDto User,
    Gender Gender
) : IRequest<Result<TokenDto>>;
