using Microsoft.AspNetCore.Http;
using JobLink.Application.Common.Interfaces;
using System.Security.Claims;
using JobLink.Domain.Common.Enums;
using System.Data;
using Dapper;

namespace JobLink.Infrastructure.Identity;

public sealed class AppUser(IHttpContextAccessor contextAccessor, ISqlConnectionFactory sqlConnectionFactory) : IAppUser
{
    public Guid? UserId
    {
        get
        {
            var id = contextAccessor.HttpContext?
                .User
                .FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return id is null ? null : Guid.Parse(id);
        }
    }

    public string? Email
    {
        get
        {
            return contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;
        }
    }

    public string? Role
    {
        get
        {
            return contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Role)?.Value;
        }
    }

    public Guid? JobSeekerId
    {
        get
        {
            if (Role == nameof(UserRole.JobSeeker))
            {
                using IDbConnection connection = sqlConnectionFactory.CreateConnection();

                string? id = connection.QueryFirstOrDefault<string?>("""
                    SELECT Id
                    FROM JobSeekerProfiles
                    WHERE UserId = @UserId
                """, new { UserId });

                return id is null ? null : Guid.Parse(id);
            }

            return null;
        }
    }

    public Guid? CompanyId
    {
        get
        {
            if (Role == nameof(UserRole.Company))
            {
                using IDbConnection connection = sqlConnectionFactory.CreateConnection();

                string? id = connection.QueryFirstOrDefault<string?>("""
                SELECT Id
                FROM Companies
                WHERE UserId = @UserId
            """, new { UserId });

                return id is null ? null : Guid.Parse(id);
            }

            return null;
        }
    }
}

