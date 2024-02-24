using Gravity.Express.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gravity.Express.Infrastructure.Persistence.Configurations;

public abstract class BaseEntityTypeConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : EntityBase
{
    protected abstract void DoConfigure(EntityTypeBuilder<TEntity> builder);

    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(x => x.Id);

        DoConfigure(builder);
    }
}
