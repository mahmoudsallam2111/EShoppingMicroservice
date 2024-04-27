using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Ordering.Domain.Abstractions;

namespace Ordering.Infrastrucure.Data.Interceptors
{
    public class AuditableEntityInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            UpdateEntities(eventData.Context);
            return base.SavingChanges(eventData, result);
        }


        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            UpdateEntities(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
        private void UpdateEntities(DbContext? context)
        {
            if (context is null) return;

            foreach (var entry in context.ChangeTracker.Entries<IEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedBy = "Mahmoud";
                    entry.Entity.CreatedAt = DateTime.UtcNow;   
                }

                if (entry.State == EntityState.Added || entry.State == EntityState.Modified || entry.HasChangesOwnedEntities())
                {
                    entry.Entity.ModifiedBy = "Mahmoud";
                    entry.Entity.ModifieddAt = DateTime.UtcNow;
                }
            }
        }
    }


    public static class Extensions
    {
        public static bool HasChangesOwnedEntities(this EntityEntry entity) =>
                entity.References.Any(
                r => r.TargetEntry != null &&
                r.TargetEntry.Metadata.IsOwned() &&
                r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified
                );
    }
}
