using Microsoft.EntityFrameworkCore;
using Promore.Core.Contexts.ClientContext.Entities;
using Promore.Core.Contexts.LotContext.Entities;
using Promore.Core.Contexts.RegionContext.Entities;
using Promore.Core.Contexts.RoleContext.Entities;
using Promore.Core.Contexts.UserContext.Entities;
using Promore.Infra.Mappings;

namespace Promore.Infra.Data;

public class PromoreDataContext : DbContext
{
    public DbSet<Client> Clients { get; set; } = null!;
    public DbSet<Lot> Lots { get; set; } = null!;
    public DbSet<Region> Regions { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;

    public PromoreDataContext() { }
    
    public PromoreDataContext(DbContextOptions<PromoreDataContext> options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ClientMap());
        modelBuilder.ApplyConfiguration(new LotMap());
        modelBuilder.ApplyConfiguration(new RegionMap());
        modelBuilder.ApplyConfiguration(new UserMap());
        modelBuilder.ApplyConfiguration(new RoleMap());
        base.OnModelCreating(modelBuilder);
    }
    
}