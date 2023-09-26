using UserService.Modules.User.Application.CreateUser;
using UserService.Modules.User.Application.FindUserByEmail;
using UserService.Modules.User.Application.UpdateUser;

namespace UserService.Config.Http
{
    public class HttpServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<CreateUserHttpController>();
            services.AddScoped<UpdateUserHttpController>();
            services.AddScoped<FindUserByEmailHttpController>();
        }
    }
}