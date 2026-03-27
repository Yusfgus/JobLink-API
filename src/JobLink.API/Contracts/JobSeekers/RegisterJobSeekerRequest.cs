using JobLink.Application.Features.JobSeekers.Commands.RegisterJobSeeker;
using JobLink.Application.Common.DTOs;
using JobLink.Domain.Common.Enums;

namespace JobLink.API.Contracts.JobSeekers;

public sealed record RegisterJobSeekerRequest(
    string Email,
    string Password,
    string FirstName,
    string LastName,
    Gender Gender
)
{
    public RegisterJobSeekerCommand ToCommand()
    {
        return new RegisterJobSeekerCommand(
            new RegisterUserDto(
                Email,
                Password
            ),
            FirstName,
            LastName,
            Gender
        );
    }
}
