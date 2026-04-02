using JobLink.Application.Common.DTOs;
using JobLink.Application.Common.Interfaces;
using JobLink.Domain.Common.Enums;
using JobLink.Domain.Common.Results;
using JobLink.Domain.JobSeekers;
using MediatR;

namespace JobLink.Application.Features.JobSeekers.Profile.Commands.RegisterJobSeeker;

public class RegisterJobSeekerCommandHandler(IAppDbContext dbContext, IUserService userService, IJwtProvider jwtProvider)
    : IRequestHandler<RegisterJobSeekerCommand, Result<TokenDto>>
{
    public async Task<Result<TokenDto>> Handle(RegisterJobSeekerCommand request, CancellationToken ct)
    {
        Result<Guid> userIdResult = await userService.RegisterUser(request.User, UserRole.JobSeeker, ct);

        if (userIdResult.IsFailure)
        {
            return userIdResult.Errors;
        }

        Guid userId = userIdResult.Value!;

        // bool mobileNumberExists = await dbContext.JobSeekerProfiles.AnyAsync(x => x.MobileNumber == request.MobileNumber, ct);

        // if (mobileNumberExists)
        // {
        //     return JobSeekerError.MobileNumberAlreadyExists;
        // }

        // Result<Address> addressResult = Address.Create(request.Address.Country, request.Address.City, request.Address.Area);

        // if (addressResult.IsFailure)
        // {
        //     return addressResult.Errors;
        // }

        Result<JobSeekerProfile> result = JobSeekerProfile.Register(userId, request.FirstName, request.LastName, request.Gender);

        if (result.IsFailure)
        {
            return result.Errors;
        }

        JobSeekerProfile jobSeekerProfile = result.Value!;

        await dbContext.JobSeekerProfiles.AddAsync(jobSeekerProfile, ct);

        var generateJWTRequest = new GenerateJWTRequest(userId, request.User.Email, UserRole.JobSeeker);

        Result<TokenDto> tokenResult = await jwtProvider.GenerateJWTAsync(generateJWTRequest, ct);

        if (tokenResult.IsFailure)
        {
            return tokenResult.Errors;
        }

        TokenDto token = tokenResult.Value!;

        await dbContext.SaveChangesAsync(ct);

        return token;
    }
}