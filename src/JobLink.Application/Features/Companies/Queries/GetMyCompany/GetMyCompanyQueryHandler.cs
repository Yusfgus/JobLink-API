using JobLink.Application.Common.Interfaces;
using JobLink.Domain.Common.Results;
using MediatR;
using JobLink.Application.Features.Companies.DTOs;
using Dapper;
using System.Data;
using JobLink.Application.Features.Identity;

namespace JobLink.Application.Features.Companies.Queries.GetMyCompany;

public class GetMyCompanyQueryHandler(ISqlConnectionFactory sqlConnectionFactory, ICurrentUser currentUser) : IRequestHandler<GetMyCompanyQuery, Result<CompanyProfileDto>>
{
    public async Task<Result<CompanyProfileDto>> Handle(GetMyCompanyQuery request, CancellationToken ct)
    {
        Guid? userId = currentUser.Id;

        if (userId is null)
        {
            // do something
            return IdentityError.UserNotFound;
        }

        using IDbConnection connection = sqlConnectionFactory.CreateConnection();

        const string sql = @"
            SELECT CP.Id as Id, CP.Name, U.Email, U.Summary
            FROM CompanyProfiles CP
            INNER JOIN Users U ON CP.UserId = U.Id
            WHERE CP.UserId = @UserId
        ";

        CompanyProfileDto? companyDto = await connection.QueryFirstOrDefaultAsync<CompanyProfileDto>(sql, new { UserId = userId });

        if (companyDto is null)
        {
            // do something
            return CompanyError.NotFound;
        }

        return companyDto;
    }
}