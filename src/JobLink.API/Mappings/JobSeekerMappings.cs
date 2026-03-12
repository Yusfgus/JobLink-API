using JobLink.Application.Features.JobSeekers.Commands.RegisterJobSeeker;
using JobLink.API.Contracts.JobSeekers;
using JobLink.Application.Common.DTOs;

namespace JobLink.API.Mappings;

public static class JobSeekerMappings
{
    public static RegisterJobSeekerCommand ToCommand(this RegisterJobSeekerRequest request)
    {
        return new RegisterJobSeekerCommand(
            new RegisterUserDto(
                request.Email,
                request.Password,
                request.ProfilePictureUrl,
                request.Summary
            ),
            request.FirstName,
            request.MiddleName,
            request.LastName,
            request.MobileNumber,
            request.BirthDate,
            new AddressDto(
                request.Country,
                request.City,
                request.Area
            ),
            request.Gender,
            request.Nationality,
            request.MilitaryStatus,
            request.MaritalStatus
        );
    }
}