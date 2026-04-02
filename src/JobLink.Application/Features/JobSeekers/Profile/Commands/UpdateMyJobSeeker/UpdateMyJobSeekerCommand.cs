using JobLink.Domain.Common.Enums;
using JobLink.Domain.Common.Results;
using JobLink.Domain.Common.ValueObjects;
using MediatR;

namespace JobLink.Application.Features.JobSeekers.Profile.Commands.UpdateMyJobSeeker;

public sealed record UpdateMyJobSeekerCommand(
    string? FirstName,
    string? MiddleName,
    string? LastName,
    string? MobileNumber,
    DateOnly? BirthDate,
    Gender? Gender,
    string? Nationality,
    MaritalStatus? MaritalStatus,
    MilitaryStatus? MilitaryStatus,
    Address Address
) : IRequest<Result>;
