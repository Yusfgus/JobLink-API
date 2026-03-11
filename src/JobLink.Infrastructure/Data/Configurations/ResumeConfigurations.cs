using JobLink.Domain.JobSeekers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using JobLink.Domain.Common.Constants;

namespace JobLink.Infrastructure.Data.Configurations;

public class ResumeConfiguration : EntityConfiguration<Resume>
{
    public override void Configure(EntityTypeBuilder<Resume> builder)
    {
        base.Configure(builder);

        builder.ToTable("Resumes");

        builder.Property(x => x.FileUrl)
            .HasMaxLength(ResumeConstraints.FileUrlMaxLength)
            .IsRequired();

        builder.HasOne(x => x.JobSeekerProfile)
            .WithOne(jp => jp.Resume)
            .HasForeignKey<Resume>(x => x.JobSeekerProfileId)
            .IsRequired();
    }
}
