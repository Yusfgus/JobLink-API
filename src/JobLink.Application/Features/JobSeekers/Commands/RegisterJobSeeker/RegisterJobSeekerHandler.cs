using JobLink.Application.Common.Interfaces;
using JobLink.Domain.Common.Enums;
using JobLink.Domain.Common.Results;
using JobLink.Domain.Common.ValueObjects;
using JobLink.Domain.JobSeekers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobLink.Application.Features.JobSeekers.Commands.RegisterJobSeeker;

public class RegisterJobSeekerHandler(IAppDbContext dbContext, IUserService userService) : IRequestHandler<RegisterJobSeekerCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(RegisterJobSeekerCommand request, CancellationToken ct)
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

        await dbContext.SaveChangesAsync(ct);

        return jobSeekerProfile.Id;
    }
}