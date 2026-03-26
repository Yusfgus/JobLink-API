using JobLink.Domain.JobSeekers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using JobLink.Domain.Common.Constants;

namespace JobLink.Infrastructure.Data.Configurations;

public class EducationConfiguration : EntityConfiguration<Education>
{
    public override void Configure(EntityTypeBuilder<Education> builder)
    {
        base.Configure(builder);

        builder.ToTable("Educations");

        builder.Property(x => x.Institution)
            .HasMaxLength(EducationConstraints.InstitutionMaxLength)
            .IsRequired();

        builder.Property(x => x.Degree)
            .HasMaxLength(EducationConstraints.DegreeMaxLength)
            .IsRequired();

        builder.Property(x => x.FieldOfStudy)
            .HasMaxLength(EducationConstraints.FieldOfStudyMaxLength)
            .IsRequired();

        builder.Property(x => x.Country)
            .HasMaxLength(EducationConstraints.CountryMaxLength)
            .IsRequired();

        builder.Property(x => x.Grade)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(x => x.StartDate)
            .IsRequired();

        builder.Property(x => x.EndDate)
            .IsRequired();

        builder.HasOne(x => x.JobSeekerProfile)
            .WithMany(x => x.Educations)
            .HasForeignKey(x => x.JobSeekerProfileId)
            .IsRequired();
    }
}
