using UserService.Modules.User.Domain.Repositories;
using UserService.Shared.Infrastructure.Persistence;

namespace UserService.Modules.User.Infrastructure.Persistence.Repositories.Implementations
{

    public class UserWriteRepository : IUserWriteRepository
    {

        private readonly Database DbContext;

        public UserWriteRepository(Database database)
        {
            DbContext = database;
        }
        public async Task Create(Domain.Entities.User user)
        {
            await DbContext.Users.AddAsync(user);
        }

        public Task Delete(Domain.Entities.User user)
        {
            throw new NotImplementedException();
        }

        public Task Update(Domain.Entities.User user)
        {
            throw new NotImplementedException();
        }
    }

}