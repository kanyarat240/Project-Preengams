using Domain.Entities.DB;
using Domain.Entities.Public;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersistensePostgresql.Configurations.Public
{
    public class PreengemConfiguration :BaseConfiguration<Preengam>
    {
        public override void Configure(EntityTypeBuilder<Preengam> builder)
        {
            base.Configure(builder);
            builder.ToTable("Preengam", "public");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

        }
    }
}



