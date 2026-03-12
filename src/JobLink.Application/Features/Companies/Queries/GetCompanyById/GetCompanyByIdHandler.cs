using JobLink.Application.Common.Interfaces;
using JobLink.Domain.Common.Results;
using MediatR;
using JobLink.Application.Common.DTOs;
using Dapper;
using System.Data;

namespace JobLink.Application.Features.Companies.Queries.GetCompanyById;

public class GetCompanyByIdHandler(ISqlConnectionFactory sqlConnectionFactory) : IRequestHandler<GetCompanyByIdQuery, Result<CompanyDto>>
{
    public async Task<Result<CompanyDto>> Handle(GetCompanyByIdQuery request, CancellationToken ct)
    {
        using IDbConnection connection = sqlConnectionFactory.CreateConnection();

        string sql = @"
            SELECT CP.Id as Id, CP.Name, U.Email, U.Summary
            FROM CompanyProfiles CP
            INNER JOIN Users U ON CP.UserId = U.Id
            WHERE CP.Id = @Id
        ";

        CompanyDto? companyDto = await connection.QueryFirstOrDefaultAsync<CompanyDto>(sql, new { Id = request.Id });

        if (companyDto is null)
        {
            return CompanyError.NotFound;
        }

        return companyDto;
    }
}