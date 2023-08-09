namespace UserService.Modules.User.Domain.Repositories
{
    using Modules.User.Domain.Entities;
    using Modules.User.Domain.ValueObjects;
    using Shared.Domain;

    public interface IUserReadRepository
    {
        Task<User?> Get(UniqueIdentity id);
        Task<User?> Get(Email email);
        Task<User?> Get(UserName userName);
        Task<IEnumerable<User>> GetAll(CancellationToken cancellationToken = default);

    }
}