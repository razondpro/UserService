using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using UserService.Shared.Domain;

namespace UserService.Shared.Infrastructure.Persistence.Core.Interceptors
{
    public class UpdateVersionedEntitiesInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            DbContext? dbContext = eventData.Context;

            if (dbContext is null)
            {
                return base.SavingChangesAsync(eventData, result, cancellationToken);
            }

            IEnumerable<EntityEntry<IVersionedEntity>> entries =
                dbContext.ChangeTracker.Entries<IVersionedEntity>();

            foreach (EntityEntry<IVersionedEntity> entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.Entity.Version++;
                        break;
                }
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

    }
}