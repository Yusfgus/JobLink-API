using JobLink.Domain.Skills;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using JobLink.Domain.Common.Constants;

namespace JobLink.Infrastructure.Data.Configurations;

public class SkillConfiguration : EntityConfiguration<Skill>
{
    public override void Configure(EntityTypeBuilder<Skill> builder)
    {
        base.Configure(builder);

        builder.ToTable("Skills");

        builder.Property(x => x.Name)
            .HasMaxLength(SkillConstraints.NameMaxLength)
            .IsRequired();

        builder.HasIndex(x => x.Name)
            .IsUnique();
    }
}
