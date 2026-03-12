using JobLink.Domain.Common.Results;
using MediatR;
using JobLink.Domain.Common.Enums;
using JobLink.Application.Common.DTOs;

namespace JobLink.Application.Features.JobSeekers.Commands.RegisterJobSeeker;

public sealed record RegisterJobSeekerCommand(
    RegisterUserDto User,
    string FirstName,
    string? MiddleName,
    string LastName,
    string? MobileNumber,
    DateOnly? BirthDate,
    AddressDto Address,
    Gender Gender,
    string? Nationality,
    MilitaryStatus? MilitaryStatus,
    MaritalStatus? MaritalStatus
) : IRequest<Result<Guid>>;
