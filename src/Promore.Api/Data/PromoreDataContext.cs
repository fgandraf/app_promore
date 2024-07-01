using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Promore.Core.Models;

namespace Promore.Api.Data;

public class PromoreDataContext(DbContextOptions<PromoreDataContext> options) : DbContext(options)
{
    public virtual DbSet<Client> Clients { get; set; }
    public virtual DbSet<Lot> Lots { get; set; }
    public virtual DbSet<Region> Regions { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Role> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}