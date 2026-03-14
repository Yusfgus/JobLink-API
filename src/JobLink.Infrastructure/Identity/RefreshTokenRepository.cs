using JobLink.Domain.Identity;
using JobLink.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace JobLink.Infrastructure.Identity;

public class RefreshTokenRepository(AppDbContext context)
{
    public async Task AddAsync(RefreshToken refreshToken, CancellationToken ct)
    {
        await context.RefreshTokens.AddAsync(refreshToken, ct);
    }

    public async Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken ct)
    {
        return await context.RefreshTokens
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Token == token, ct);
    }

    public async Task RemoveAllAsync(Guid userId, CancellationToken ct)
    {
        await context.RefreshTokens
              .Where(rt => rt.UserId == userId)
              .ExecuteDeleteAsync(ct);
    }
}