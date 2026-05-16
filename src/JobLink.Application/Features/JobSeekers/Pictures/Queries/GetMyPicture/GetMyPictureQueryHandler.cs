using Dapper;
using JobLink.Application.Common.Interfaces;
using JobLink.Application.Features.Identity;
using JobLink.Domain.Common.Results;
using MediatR;

namespace JobLink.Application.Features.JobSeekers.Pictures.Queries.GetMyPicture;

public class GetMyPictureQueryHandler(ISqlConnectionFactory sqlConnectionFactory, IAppUser appUser) : IRequestHandler<GetMyPictureQuery, Result<string>>
{
    public async Task<Result<string>> Handle(GetMyPictureQuery request, CancellationToken cancellationToken)
    {
        Guid? userId = appUser.UserId;
        if (userId is null)
        {
            return IdentityError.Unauthenticated;
        }

        const string sql = @"
            SELECT jsp.ProfilePictureUrl 
            FROM JobSeekerProfiles jsp
            WHERE jsp.UserId = @UserId";

        var connection = sqlConnectionFactory.CreateConnection();

        var pictureUrl = await connection.QueryFirstOrDefaultAsync<string>(sql, new { UserId = userId });

        return pictureUrl ?? string.Empty;
    }
}