using Dapper;
using JobLink.Application.Common.Interfaces;
using JobLink.Application.Features.Identity;
using JobLink.Domain.Common.Results;
using MediatR;

namespace JobLink.Application.Features.JobSeekers.Resumes.Queries.DownloadMyResume;

public class DownloadMyResumeQueryHandler(ISqlConnectionFactory sqlConnectionFactory, IAppUser appUser, IFileStorageService fileStorageService) : IRequestHandler<DownloadMyResumeQuery, Result<Stream>>
{
    public async Task<Result<Stream>> Handle(DownloadMyResumeQuery request, CancellationToken cancellationToken)
    {
        Guid? userId = appUser.UserId;
        if (userId is null)
        {
            return IdentityError.Unauthenticated;
        }

        const string sql = @"
            SELECT r.FileUrl 
            FROM Resumes r
            JOIN JobSeekerProfiles jsp ON r.JobSeekerProfileId = jsp.Id
            JOIN Users u ON jsp.UserId = u.Id
            WHERE u.Id = @UserId";

        var connection = sqlConnectionFactory.CreateConnection();

        var resumeUrl = await connection.QueryFirstOrDefaultAsync<string>(sql,
            new { UserId = userId });

        if (resumeUrl is null)
        {
            return Error.NotFound("Resume not found");
        }

        var result = await fileStorageService.GetAsync(resumeUrl, cancellationToken);
        if (result.IsFailure)
        {
            return result.Errors;
        }

        Stream stream = result.Value!;

        return stream;
    }
}