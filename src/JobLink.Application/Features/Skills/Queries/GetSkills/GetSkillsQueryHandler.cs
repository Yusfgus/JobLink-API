using JobLink.Domain.Common.Results;
using JobLink.Application.Features.Skills.DTOs;
using MediatR;
using JobLink.Application.Common.Interfaces;
using System.Data;
using Dapper;

namespace JobLink.Application.Features.Skills.Queries.GetSkills;

public sealed class GetSkillsQueryHandler(ISqlConnectionFactory sqlConnectionFactory) : IRequestHandler<GetSkillsQuery, Result<List<SkillDto>>>
{
    public async Task<Result<List<SkillDto>>> Handle(GetSkillsQuery request, CancellationToken ct)
    {
        using IDbConnection connection = sqlConnectionFactory.CreateConnection();

        const string sql = "SELECT Id, Name FROM Skills";

        var command = new CommandDefinition(commandText: sql, cancellationToken: ct);

        return (await connection.QueryAsync<SkillDto>(command)).ToList();
    }
}