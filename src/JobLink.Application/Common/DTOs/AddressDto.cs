using JobLink.Domain.Common.ValueObjects;

namespace JobLink.Application.Common.DTOs;

public sealed record AddressDto(
    string? Country,
    string? City,
    string? Area
);
