using JobLink.Application.Common.Interfaces;
using JobLink.Domain.Common.Results;
using MediatR;
using JobLink.Application.Common.DTOs;
using Dapper;
using System.Data;

namespace JobLink.Application.Features.Companies.Queries.GetCompanyById;

public class GetCompanyByIdHandler(ISqlConnectionFactory sqlConnectionFactory) : IRequestHandler<GetCompanyByIdQuery, Result<CompanyProfileDto>>
{
    public async Task<Result<CompanyProfileDto>> Handle(GetCompanyByIdQuery request, CancellationToken ct)
    {
        using IDbConnection connection = sqlConnectionFactory.CreateConnection();

        const string sql = @"
            SELECT CP.Id as Id, CP.Name, U.Email, U.Summary
            FROM CompanyProfiles CP
            INNER JOIN Users U ON CP.UserId = U.Id
            WHERE CP.Id = @Id
        ";

        CompanyProfileDto? companyDto = await connection.QueryFirstOrDefaultAsync<CompanyProfileDto>(sql, new { Id = request.Id });

        if (companyDto is null)
        {
            // do something
            return CompanyError.NotFound;
        }

        return companyDto;
    }
}