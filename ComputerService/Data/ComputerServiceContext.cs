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
    public DbSet<User> Users { get; set; }
}
