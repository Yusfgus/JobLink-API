using JobLink.Domain.JobSeekers.Educations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobLink.Infrastructure.Data.Configurations;

public class EducationConfiguration : EntityConfiguration<Education>
{
    public override void Configure(EntityTypeBuilder<Education> builder)
    {
        base.Configure(builder);

        builder.ToTable("Educations");

        builder.Property(x => x.Institution)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(x => x.Degree)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.FieldOfStudy)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Country)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Grade)
            .HasConversion<string>()
            .IsRequired();

        builder.HasOne(x => x.JobSeekerProfile)
            .WithMany(x => x.Educations)
            .HasForeignKey(x => x.JobSeekerProfileId)
            .IsRequired();
    }
}
