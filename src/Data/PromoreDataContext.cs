using Microsoft.EntityFrameworkCore;
using PromoreApi.Data.Mappings;
using PromoreApi.Models;

namespace PromoreApi.Data;

public class PromoreDataContext : DbContext
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<Lot> Lots { get; set; }
    public DbSet<Professional> Professionals { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer("Server=localhost,1433;Database=Promore;User ID=sa;Password=1q2w3e4r@#$;Encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ClientMap());
        modelBuilder.ApplyConfiguration(new LotMap());
        modelBuilder.ApplyConfiguration(new ProfessionalMap());
        modelBuilder.ApplyConfiguration(new RegionMap());
        modelBuilder.ApplyConfiguration(new UserMap());
        modelBuilder.ApplyConfiguration(new RoleMap());
    }
}