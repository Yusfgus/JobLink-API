using JobLink.Application.Features.JobSeekers.Profile.Commands.UpdateMyJobSeeker;
using JobLink.Domain.Common.Enums;

namespace JobLink.API.Contracts.JobSeekers;

public sealed record UpdateJobSeekerRequest(
    string? FirstName,
    string? MiddleName,
    string? LastName,
    string? MobileNumber,
    DateOnly? BirthDate,
    Gender? Gender,
    string? Nationality,
    MaritalStatus? MaritalStatus,
    MilitaryStatus? MilitaryStatus,
    string? Country,
    string? City,
    string? Area,
    string? ProfilePictureUrl,
    string? Summary
)
{
    public UpdateMyJobSeekerCommand ToCommand()
    {
        return new UpdateMyJobSeekerCommand(
            FirstName,
            MiddleName,
            LastName,
            MobileNumber,
            BirthDate,
            Gender,
            Nationality,
            MaritalStatus,
            MilitaryStatus,
            new(Country, City, Area),
            ProfilePictureUrl,
            Summary
        );
    }
}
