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
                request.Password
            ),
            request.FirstName,
            request.LastName,
            request.Gender
        );
    }
}