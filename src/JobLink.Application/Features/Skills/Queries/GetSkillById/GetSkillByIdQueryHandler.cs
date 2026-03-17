using JobLink.Domain.Common.Results;
using JobLink.Application.Features.Skills.DTOs;
using MediatR;
using JobLink.Application.Common.Interfaces;
using System.Data;
using Dapper;

namespace JobLink.Application.Features.Skills.Queries.GetSkillById;

public sealed class GetSkillByIdQueryHandler(ISqlConnectionFactory sqlConnectionFactory) : IRequestHandler<GetSkillByIdQuery, Result<SkillDto>>
{
    public async Task<Result<SkillDto>> Handle(GetSkillByIdQuery request, CancellationToken ct)
    {
        using IDbConnection connection = sqlConnectionFactory.CreateConnection();

        const string sql = "SELECT Id, Name FROM Skills WHERE Id = @Id";

        var command = new CommandDefinition(commandText: sql, parameters: new { Id = request.Id }, cancellationToken: ct);

        SkillDto? skill = await connection.QueryFirstOrDefaultAsync<SkillDto>(command);

        return skill is null ? Error.NotFound("Skill not found") : skill;
    }
}