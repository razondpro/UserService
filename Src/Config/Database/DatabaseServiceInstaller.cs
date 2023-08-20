namespace UserService.Config.Database
{
    using UserService.Modules.User.Domain.Repositories;
    using UserService.Modules.User.Infrastructure.Persistence.Repositories;
    public class DatabaseServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabase();
            services.AddScoped<IUserReadRepository, UserReadRepository>();
            services.AddScoped<IUserWriteRepository, UserWriteRepository>();
        }
    }
}