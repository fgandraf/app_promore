using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Promore.Core.Models;

namespace Promore.Api.Data.Contexts;

public class PromoreDataContext : DbContext
{
    public virtual DbSet<Client> Clients { get; set; }
    public virtual DbSet<Lot> Lots { get; set; }
    public virtual DbSet<Region> Regions { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Role> Roles { get; set; }
    
    public PromoreDataContext(DbContextOptions<PromoreDataContext> options) : base(options) { }
    
    public PromoreDataContext() : base(new DbContextOptions<PromoreDataContext>()) { }
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}