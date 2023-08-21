namespace UserService.Config.Database
{
    using UserService.Modules.User.Domain.Repositories;
    using UserService.Modules.User.Infrastructure.Persistence.Repositories;
    using UserService.Shared.Infrastructure.Persistence.Core;

    public class DatabaseServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabase();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserReadRepository, UserReadRepository>();
            services.AddScoped<IUserWriteRepository, UserWriteRepository>();
        }
    }
}