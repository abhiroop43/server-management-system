using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ServerManagement.Infrastructure.Data;

public class ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options)
    : IdentityDbContext(options)
{
    private const string DefaultSchema = "ServerMgmt";
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        builder.HasDefaultSchema(DefaultSchema);
        base.OnModelCreating(builder);
    }
}
