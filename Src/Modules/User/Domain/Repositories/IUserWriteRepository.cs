namespace UserService.Modules.User.Domain.Repositories
{
    using Modules.User.Domain.Entities;
    public interface IUserWriteRepository
    {
        Task Create(User user);
        Task Update(User user);
        Task Delete(User user);
    }
}