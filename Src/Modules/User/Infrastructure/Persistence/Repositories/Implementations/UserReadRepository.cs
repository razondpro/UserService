namespace UserService.Modules.User.Infrastructure.Persistence.Repositories.Implementations
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Modules.User.Domain.Entities;
    using Modules.User.Domain.Repositories;
    using Shared.Infrastructure.Persistence;
    using Modules.User.Domain.ValueObjects;
    using Shared.Domain;
    using Microsoft.EntityFrameworkCore;

    public class UserReadRepository : IUserReadRepository
    {
        private readonly Database DbContext;

        public UserReadRepository(Database database)
        {
            DbContext = database;
        }

        public async Task<User?> Get(UniqueIdentity id)
        {
            return await DbContext.Users.FindAsync(id.Value);
        }

        public async Task<User?> Get(Email email)
        {
            return await DbContext.Users.FirstOrDefaultAsync(u => ((string)u.Email).Equals(email.Value));
        }

        public async Task<User?> Get(UserName userName)
        {
            return await DbContext.Users
                .Where(u => ((string)u.UserName).Equals(userName.Value))
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<User>> GetAll(CancellationToken cancellationToken = default)
        {
            return await DbContext.Users.ToListAsync(cancellationToken);
        }
    }
}