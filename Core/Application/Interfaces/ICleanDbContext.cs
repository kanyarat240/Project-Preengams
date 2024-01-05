
using Domain.Entities.DB;
using Domain.Entities.Public;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces;

public interface ICleanDbContext : IDbContext
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
}

