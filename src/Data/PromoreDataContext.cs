using Microsoft.EntityFrameworkCore;
using PromoreApi.Models;

namespace PromoreApi.Data;

public class PromoreDataContext : DbContext
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<Lot> Lots { get; set; }
    public DbSet<Professional> Professionals { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<User> Users { get; set; }

    private string _connectionString;

    public PromoreDataContext(string connection)
        => _connectionString = connection;
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer(_connectionString);
    
}