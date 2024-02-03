using Microsoft.EntityFrameworkCore;
using Promore.Api.Data.Mappings;
using Promore.Core.Entities;

namespace Promore.Api.Data;

public class PromoreDataContext : DbContext
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<Lot> Lots { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }


    public PromoreDataContext(DbContextOptions<PromoreDataContext> options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ClientMap());
        modelBuilder.ApplyConfiguration(new LotMap());
        modelBuilder.ApplyConfiguration(new RegionMap());
        modelBuilder.ApplyConfiguration(new UserMap());
        modelBuilder.ApplyConfiguration(new RoleMap());
    }
}