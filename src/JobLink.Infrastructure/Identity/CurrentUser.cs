using Microsoft.AspNetCore.Http;
using JobLink.Application.Common.Interfaces;
using System.Security.Claims;

namespace JobLink.Infrastructure.Identity;

public sealed class CurrentUser(IHttpContextAccessor contextAccessor) : ICurrentUser
{
    public Guid? Id
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
}

