using JobLink.Domain.JobSeekers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobLink.Infrastructure.Data.Configurations;

public class JobSeekerSkillConfiguration : EntityConfiguration<JobSeekerSkill>
{
    public override void Configure(EntityTypeBuilder<JobSeekerSkill> builder)
    {
        base.Configure(builder);

        builder.ToTable("JobSeekerSkills");

        builder.Property(x => x.SkillLevel)
            .HasConversion<string>()
            .IsRequired();

        builder.HasOne(x => x.JobSeekerProfile)
            .WithMany(x => x.Skills)
            .HasForeignKey(x => x.JobSeekerProfileId)
            .IsRequired();

        builder.HasOne(x => x.Skill)
            .WithMany()
            .HasForeignKey(x => x.SkillId)
            .IsRequired();

        builder.HasIndex(x => new { x.JobSeekerProfileId, x.SkillId })
            .IsUnique();
    }
}
