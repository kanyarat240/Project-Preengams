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
    public class UserConfiguration :BaseConfiguration<UserAccount>
    {
        public override void Configure(EntityTypeBuilder<UserAccount> builder)
        {
            base.Configure(builder);
            builder.ToTable("UserAccount", "db");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

        }
    }
}



