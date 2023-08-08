namespace UserService.Modules.User.Domain.Repositories
{
    using Modules.User.Domain.Entities;
    using Modules.User.Domain.ValueObjects;
    using Shared.Domain;

    public interface IUserReadRepository
    {
        Task<User?> Get(UniqueIdentity id, CancellationToken cancellationToken = default);
        Task<User?> Get(Email email, CancellationToken cancellationToken = default);
        Task<User?> Get(UserName userName, CancellationToken cancellationToken = default);
        Task<IEnumerable<User>> GetAll(CancellationToken cancellationToken = default);

    }
}