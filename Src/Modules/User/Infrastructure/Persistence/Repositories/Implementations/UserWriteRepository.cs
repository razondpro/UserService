namespace UserService.Modules.User.Infrastructure.Persistence.Repositories.Implementations
{
    using UserService.Modules.User.Domain.Repositories;
    using UserService.Modules.User.Domain.Entities;
    using UserService.Shared.Infrastructure.Persistence;

    public class UserWriteRepository : IUserWriteRepository
    {

        private readonly Database DbContext;

        public UserWriteRepository(Database database)
        {
            DbContext = database;
        }
        public async Task Create(User user)
        {
            await DbContext.Users.AddAsync(user);
        }

        public Task Delete(User user)
        {
            throw new NotImplementedException();
        }

        public Task Update(User user)
        {
            DbContext.Users.Update(user);
            return Task.CompletedTask;
        }
    }

}