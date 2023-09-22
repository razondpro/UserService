using UserService.Modules.User.Application.CreateUser;
using UserService.Modules.User.Application.FindUserByEmail;

namespace UserService.Config.Http
{
    public class HttpServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<CreateUserHttpController>();
            services.AddScoped<FindUserByEmailHttpController>();
        }
    }
}