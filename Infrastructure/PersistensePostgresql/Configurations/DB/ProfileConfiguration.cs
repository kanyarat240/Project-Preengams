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
    public class ProfileConfiguration : BaseConfiguration<Profiles>
    {
        public override void Configure(EntityTypeBuilder<Profiles> builder)
        {
            base.Configure(builder);
            builder.ToTable("Profile", "db");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.HasOne(e => e.Rooms).WithMany(e => e.Profiles).HasForeignKey(e => e.RoomId);
            builder.HasOne(e => e.User).WithMany(e => e.Profiles).HasForeignKey(e => e.AccountId);


        }
    }
}
