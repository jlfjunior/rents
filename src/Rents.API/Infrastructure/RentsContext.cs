using Microsoft.EntityFrameworkCore;
using Rents.API.Domain;

namespace Rents.API.Infrastructure;

public class RentsContext : DbContext
{
    public RentsContext(DbContextOptions<RentsContext> options)
        : base(options)
            { }

    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<Driver> Drivers { get; set; }
    public DbSet<Location> Locations { get; set; }
}