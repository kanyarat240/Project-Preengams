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
    public class BillConfiguration : BaseConfiguration<Bill>
    {
        public override void Configure(EntityTypeBuilder<Bill> builder)
        {
            base.Configure(builder);
            builder.ToTable("Bill", "db");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.HasOne(e => e.Profiles).WithMany(e => e.Bills).HasForeignKey(e => e.ProfileId);
            builder.HasOne(e => e.Waters).WithMany(e => e.Bills).HasForeignKey(e => e.WaterId);
            builder.HasOne(e => e.Electricity).WithMany(e => e.Bills).HasForeignKey(e => e.ElectricityId);
            builder.HasOne(e => e.Status).WithMany(e => e.Bills).HasForeignKey(e => e.StatusId);

        }
    }
}
