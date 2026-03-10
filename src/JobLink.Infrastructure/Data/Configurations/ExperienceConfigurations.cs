using JobLink.Domain.JobSeekers.Experiences;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobLink.Infrastructure.Data.Configurations;

public class ExperienceConfiguration : EntityConfiguration<Experience>
{
    public override void Configure(EntityTypeBuilder<Experience> builder)
    {
        base.Configure(builder);

        builder.ToTable("Experiences");

        builder.Property(x => x.Company)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(x => x.Position)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Country)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(2000);

        builder.Property(x => x.Salary)
            .HasColumnType("decimal(18,2)");

        builder.HasOne(x => x.JobSeekerProfile)
            .WithMany(x => x.Experiences)
            .HasForeignKey(x => x.JobSeekerProfileId)
            .IsRequired();
    }
}
