namespace UserService.Shared.Infrastructure.Persistence.Core
{
    public interface IUnitOfWork
    {
        Task CommitAsync(CancellationToken cancellationToken = default);
    }
}