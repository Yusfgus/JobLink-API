using JobLink.Domain.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace JobLink.Infrastructure.Data.Configurations;

public abstract class EntityConfiguration<T> : IEntityTypeConfiguration<T> where T : Entity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();
    }
}
