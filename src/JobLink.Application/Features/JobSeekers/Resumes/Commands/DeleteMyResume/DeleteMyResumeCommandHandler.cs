using JobLink.Application.Common.Interfaces;
using JobLink.Application.Features.Identity;
using JobLink.Domain.Common.Results;
using JobLink.Domain.JobSeekers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobLink.Application.Features.JobSeekers.Resumes.Commands.DeleteMyResume;

public class DeleteMyResumeCommandHandler(IAppDbContext dbContext, IAppUser appUser, IFileStorageService fileStorageService) : IRequestHandler<DeleteMyResumeCommand, Result>
{
    public async Task<Result> Handle(DeleteMyResumeCommand request, CancellationToken cancellationToken)
    {
        Guid? userId = appUser.UserId;
        if (userId is null)
        {
            return IdentityError.Unauthenticated;
        }

        Resume? resume = await dbContext.Resumes.FirstOrDefaultAsync(r => r.JobSeekerProfile!.UserId == userId, cancellationToken);
        if (resume is null)
        {
            return Result.Failure(Error.NotFound("Resume not found"));
        }

        var result = await fileStorageService.DeleteAsync(resume.FileUrl, cancellationToken);
        if (result.IsFailure)
        {
            return result;
        }

        dbContext.Resumes.Remove(resume);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
