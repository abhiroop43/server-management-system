using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ServerManagement.Domain.Abstractions;
using ServerManagement.Infrastructure.Data.Extensions;

namespace ServerManagement.Infrastructure.Data.Interceptors;

public class AuditableEntityInterceptor(IHttpContextAccessor httpContextAccessor)
    : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result
    )
    {
        UpdateEntities(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken()
    )
    {
        UpdateEntities(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateEntities(DbContext? dbContext)
    {
        if (dbContext == null)
        {
            return;
        }

        var user = httpContextAccessor.HttpContext?.User;
        var username =
            user?.Identity?.Name
            ?? user?.FindFirst("preferred_username")?.Value
            ?? user?.FindFirst("email")?.Value
            ?? "System";

        foreach (var entry in dbContext.ChangeTracker.Entries<IEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedDate = DateTime.UtcNow;
                entry.Entity.CreatedBy = username;
            }

            if (
                entry.State != EntityState.Modified
                && entry.State != EntityState.Added
                && !entry.HasChangedOwnEntities()
            )
            {
                continue;
            }

            entry.Entity.UpdatedDate = DateTime.UtcNow;
            entry.Entity.UpdatedBy = username;
        }
    }
}
