namespace UserService.Modules.User.Infrastructure.Persistence.Repositories
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Modules.User.Domain.Entities;
    using Modules.User.Domain.Repositories;
    using Shared.Infrastructure.Persistence;
    using Modules.User.Domain.ValueObjects;
    using Shared.Domain;
    public class UserReadRepository : IUserReadRepository
    {
        private readonly Database DbContext;

        public UserReadRepository(Database database)
        {
            DbContext = database;
        }

        public Task<User?> Get(UniqueIdentity id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<User?> Get(Email email, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<User?> Get(UserName userName, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAll(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}