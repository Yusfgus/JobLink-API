using Dapper;
using JobLink.Application.Common.Interfaces;
using JobLink.Application.Features.Identity;
using JobLink.Application.Features.JobSeekers.DTOs;
using JobLink.Domain.Common.Results;
using MediatR;

namespace JobLink.Application.Features.JobSeekers.Resumes.Queries.GetMyResume;

public class GetMyResumeQueryHandler(ISqlConnectionFactory sqlConnectionFactory, IAppUser appUser) : IRequestHandler<GetMyResumeQuery, Result<ResumeDto>>
{
    public async Task<Result<ResumeDto>> Handle(GetMyResumeQuery request, CancellationToken cancellationToken)
    {
        Guid? userId = appUser.UserId;
        if (userId is null)
        {
            return IdentityError.Unauthenticated;
        }

        const string sql = @"
            SELECT r.Id, r.FileUrl as ResumeUrl
            FROM Resumes r
            JOIN JobSeekerProfiles jsp ON r.JobSeekerProfileId = jsp.Id
            WHERE jsp.UserId = @UserId";

        var connection = sqlConnectionFactory.CreateConnection();

        var resume = await connection.QueryFirstOrDefaultAsync<ResumeDto>(sql,
            new { UserId = userId });

        if (resume is null)
        {
            return Error.NotFound("Resume not found");
        }

        Console.WriteLine("Resume: " + resume.Id);
        Console.WriteLine("Resume URL: " + resume.ResumeUrl);

        return resume;
    }
}