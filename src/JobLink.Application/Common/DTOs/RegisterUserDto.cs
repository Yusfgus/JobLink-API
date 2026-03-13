namespace JobLink.Application.Common.DTOs;

public sealed record RegisterUserDto(
    string Email,
    string Password
);