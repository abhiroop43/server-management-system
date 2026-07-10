using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ServerManagement.Infrastructure.Data.Extensions;

public static class DatabaseExtensions
{
    public static bool HasChangedOwnEntities(this EntityEntry entry)
    {
        return entry.References.Any(r =>
            r.TargetEntry != null
            && r.TargetEntry.Metadata.IsOwned()
            && r.TargetEntry.State is EntityState.Added or EntityState.Modified
        );
    }

    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await dbContext.Database.MigrateAsync();
        // await SeedAsync(dbContext);
    }
}
