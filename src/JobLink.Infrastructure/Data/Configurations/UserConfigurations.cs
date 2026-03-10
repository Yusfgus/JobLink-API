using JobLink.Domain.Users;
using JobLink.Domain.Common.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace JobLink.Infrastructure.Data.Configurations;

public class UserConfiguration : EntityConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder.ToTable("Users");

        builder.Property(x => x.Email)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.PasswordHash)
            .IsRequired();

        builder.Property(x => x.ProfilePictureUrl)
            .HasMaxLength(255);

        builder.Property(x => x.Summary)
            .HasMaxLength(500);

        builder.Property(x => x.Role)
            .HasConversion<string>()
            .IsRequired();
    }
}
