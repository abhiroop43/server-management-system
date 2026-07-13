using System.Reflection;
using ServerManagement.Domain.Entities;

namespace ServerManagement.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options)
{
    private const string DefaultSchema = "ServerMgmt";
    public DbSet<Disk> Disks => Set<Disk>();
    public DbSet<HostedService> HostedServices => Set<HostedService>();
    public DbSet<Server> Servers => Set<Server>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.HasDefaultSchema(DefaultSchema);
        base.OnModelCreating(modelBuilder);
    }
}
