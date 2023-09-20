namespace UserService.Config.Mediatr
{
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;
    using UserService.Config;
    using UserService.Modules.User.Application.Behaviors;
    using UserService.Shared.Infrastructure.Idempotence;

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

            services.Decorate(typeof(INotificationHandler<>), typeof(IdempotentDomainEventHandler<>));
        }
    }
}