using JobLink.Domain.Common.Enums;

namespace JobLink.Application.Common.DTOs;

public sealed record TokenDto
(
    string AccessToken,
    string RefreshToken,
    DateTime Expires,
    UserRole Role
);