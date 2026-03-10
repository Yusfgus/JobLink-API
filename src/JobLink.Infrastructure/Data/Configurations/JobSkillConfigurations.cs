using JobLink.Domain.Companies.Jobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobLink.Infrastructure.Data.Configurations;

public class JobSkillConfiguration : EntityConfiguration<JobSkill>
{
    public override void Configure(EntityTypeBuilder<JobSkill> builder)
    {
        base.Configure(builder);

        builder.ToTable("JobSkills");

        builder.HasOne(x => x.Job)
            .WithMany(x => x.Skills)
            .HasForeignKey(x => x.JobId)
            .IsRequired();

        builder.HasOne(x => x.Skill)
            .WithMany()
            .HasForeignKey(x => x.SkillId)
            .IsRequired();
    }
}
