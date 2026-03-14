using JobLink.Domain.Identity;
using JobLink.Domain.Common.Constants;
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
            .HasMaxLength(UserConstraints.EmailMaxLength)
            .IsRequired();

        builder.Property(x => x.PasswordHash)
            .HasMaxLength(UserConstraints.PasswordHashMaxLength)
            .IsRequired();

        builder.Property(x => x.ProfilePictureUrl)
            .HasMaxLength(UserConstraints.ProfilePictureUrlMaxLength);

        builder.Property(x => x.Summary)
            .HasMaxLength(UserConstraints.SummaryMaxLength);

        builder.Property(x => x.Role)
            .HasConversion<string>()
            .IsRequired();

        builder.HasIndex(x => x.Email)
            .IsUnique();
    }
}
