using JobLink.Application.Common.Interfaces;
using JobLink.Application.Features.Identity;
using JobLink.Domain.Common.Results;
using JobLink.Domain.JobSeekers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobLink.Application.Features.JobSeekers.Pictures.Commands.DeleteMyPicture;

public class DeleteMyPictureCommandHandler(IAppDbContext dbContext, IAppUser appUser, IFileStorageService fileStorageService) : IRequestHandler<DeleteMyPictureCommand, Result>
{
    public async Task<Result> Handle(DeleteMyPictureCommand request, CancellationToken cancellationToken)
    {
        Guid? userId = appUser.UserId;
        if (userId is null)
        {
            return IdentityError.Unauthenticated;
        }

        JobSeekerProfile? profile = await dbContext.JobSeekerProfiles.FirstOrDefaultAsync(p => p.UserId == userId, cancellationToken);
        if (profile is null)
        {
            return Result.Failure(Error.NotFound("Profile not found"));
        }

        string? pictureUrl = profile.ProfilePictureUrl;
        if (pictureUrl is null)
        {
            return Result.Failure(Error.NotFound("Picture not found"));
        }

        var result = await fileStorageService.DeleteAsync(pictureUrl, cancellationToken);
        if (result.IsFailure)
        {
            return result;
        }

        profile.RemoveProfilePicture();
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
