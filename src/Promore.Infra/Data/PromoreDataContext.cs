using Microsoft.EntityFrameworkCore;
using Promore.Core.Entities;
using Promore.Infra.Data.Mappings;

namespace Promore.Infra.Data;

public class PromoreDataContext : DbContext
{
    public DbSet<Client> Clients { get; set; } = null!;
    public DbSet<Lot> Lots { get; set; } = null!;
    public DbSet<Region> Regions { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;

    public PromoreDataContext()
    {
        
    }
    
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