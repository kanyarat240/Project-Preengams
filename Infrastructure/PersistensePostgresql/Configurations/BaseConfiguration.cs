using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PersistensePostgresql.Configurations;

public class BaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : EntityBase
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.Property(e => e.CreatedBy).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        builder.Property(e => e.CreatedDateTime).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        builder.Property(e => e.CreatedProgramCode).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        //builder.Ignore(e => e.Guid);
        //builder.Ignore(e => e.RowState);
        //builder.Ignore(e => e.RowVersion);
        //    builder.Property(e => e.RowVersion).HasColumnName("xmin").HasColumnType("xid").ValueGeneratedOnAddOrUpdate().IsConcurrencyToken();
    }
}