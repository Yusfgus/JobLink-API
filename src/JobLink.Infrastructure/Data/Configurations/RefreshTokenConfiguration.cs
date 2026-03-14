using JobLink.Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobLink.Infrastructure.Data.Configurations;

public class RefreshTokenConfiguration : EntityConfiguration<RefreshToken>
{
    public override void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("RefreshTokens");

        builder.Property(rt => rt.Token)
            .HasMaxLength(200);

        builder.HasIndex(rt => rt.Token)
            .IsUnique();

        builder.HasOne(x => x.User)
            .WithOne()
            .HasForeignKey<RefreshToken>(x => x.UserId)
            .IsRequired();

        builder.Property(rt => rt.ExpiresOnUtc)
            .IsRequired();
    }
}