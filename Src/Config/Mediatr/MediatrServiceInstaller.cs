namespace UserService.Config.Mediatr
{
    using Microsoft.Extensions.DependencyInjection;
    using UserService.Config;
    using UserService.Modules.User.Application.Abstractions.Behaviors;

    public class MediatrServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg =>
                {
                    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
                    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
                    cfg.AddOpenBehavior(typeof(UnitOfWorkBehavior<,>));
                });
        }
    }
}