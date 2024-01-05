using Domain.Entities.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersistensePostgresql.Configurations.DB
{
    public class ElectricityConmfiguration : BaseConfiguration<Electricity>
    {
        public override void Configure(EntityTypeBuilder<Electricity> builder)
        {
            base.Configure(builder);
            builder.ToTable("Electricity", "db");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.HasOne(e => e.Rooms).WithMany(e => e.Electricity).HasForeignKey(e => e.RoomId);

        }
    }
}
