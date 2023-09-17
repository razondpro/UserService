namespace UserService.Shared.Infrastructure.Persistence.Core.Interceptors
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Microsoft.EntityFrameworkCore.Diagnostics;
    using UserService.Shared.Domain;


    public class UpdateAuditableEntitiesInterceptor : SaveChangesInterceptor
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

            IEnumerable<EntityEntry<IAuditableEntity>> entries =
                dbContext.ChangeTracker.Entries<IAuditableEntity>();

            foreach (EntityEntry<IAuditableEntity> entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedOn = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedOn = DateTime.UtcNow;
                        break;
                }
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

    }
}