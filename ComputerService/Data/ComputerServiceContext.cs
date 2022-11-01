using ComputerService.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ComputerService.Data;

public class ComputerServiceContext : DbContext
{
    public ComputerServiceContext(DbContextOptions<ComputerServiceContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public DbSet<Accessory> Accessories { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Device> Devices { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderAccessory> OrderAccessories { get; set; }
    public DbSet<OrderDetails> OrdersDetails { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserTracking> UserTrackings { get; set; }
}
