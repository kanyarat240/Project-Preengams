using Application.Interfaces;
using Domain.Entities.DB;
using Domain.Entities.Public;
using Line.Messaging;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PersistensePostgresql;

public partial class CleanDbContext : DbContext, ICleanDbContext
{

    //DB
    public DbSet<UserAccount> UserAccount { get; set; }
    public DbSet<Profiles> Profile { get; set; }
    public DbSet<Room> Room { get; set; }
    public DbSet<Status> Status { get; set; }
    public DbSet<Water> Water { get; set; }
    public DbSet<Electricity> Electricity { get; set; }
    public DbSet<Bill> Bill { get; set; }
    public DbSet<Payment> Payment { get; set; }

    //Public
    public DbSet<Preengam> Preengam { get; set; }
    public DbSet<Rules> Rules { get; set; }
}

