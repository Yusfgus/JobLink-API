using JobLink.Domain.JobSeekers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using JobLink.Domain.Common.Constants;

namespace JobLink.Infrastructure.Data.Configurations;

public class ExperienceConfiguration : EntityConfiguration<Experience>
{
    public override void Configure(EntityTypeBuilder<Experience> builder)
    {
        base.Configure(builder);

        builder.ToTable("Experiences");

        builder.Property(x => x.Company)
            .HasMaxLength(ExperienceConstraints.CompanyNameMaxLength)
            .IsRequired();

        builder.Property(x => x.Position)
            .HasMaxLength(ExperienceConstraints.PositionMaxLength)
            .IsRequired();

        builder.Property(x => x.Country)
            .HasMaxLength(ExperienceConstraints.CountryMaxLength)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(ExperienceConstraints.DescriptionMaxLength);

        builder.Property(x => x.Salary)
            .HasPrecision(18, 2);

        builder.HasOne(x => x.JobSeekerProfile)
            .WithMany(x => x.Experiences)
            .HasForeignKey(x => x.JobSeekerProfileId)
            .IsRequired();
    }
}
