using JobLink.Application.Common.Interfaces;
using JobLink.Application.Features.JobSeekers.DTOs;
using JobLink.Domain.Common.Results;
using JobLink.Application.Features.Identity;
using MediatR;
using Dapper;
using System.Data;

namespace JobLink.Application.Features.JobSeekers.Experiences.Queries.GetMyExperienceById;

public sealed class GetMyExperienceByIdQueryHandler(ISqlConnectionFactory sqlConnectionFactory, IAppUser appUser) : IRequestHandler<GetMyExperienceByIdQuery, Result<ExperienceDto>>
{
    public async Task<Result<ExperienceDto>> Handle(GetMyExperienceByIdQuery request, CancellationToken cancellationToken)
    {
        Guid? userId = appUser.UserId;
        if (userId is null)
        {
            return IdentityError.Unauthenticated;
        }

        using IDbConnection connection = sqlConnectionFactory.CreateConnection();

        const string sql = @"
            SELECT 
                E.Id,
                E.Company,
                E.Position,
                E.Country,
                E.Description,
                E.Salary,
                E.StartDate,
                E.EndDate
            FROM Experiences E
            INNER JOIN JobSeekerProfiles JSP ON E.JobSeekerProfileId = JSP.Id
            WHERE E.Id = @ExperienceId AND JSP.UserId = @UserId
        ";

        ExperienceDto? experienceDto = await connection.QueryFirstOrDefaultAsync<ExperienceDto>(sql, new { ExperienceId = request.Id, UserId = userId.Value });

        if (experienceDto is null)
        {
            return Error.NotFound("Experience.NotFound", "The experience with the specified ID was not found.");
        }

        return experienceDto;
    }
}
