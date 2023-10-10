namespace UserService.Config.Mediatr
{
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;
    using UserService.Config;
    using UserService.Shared.Infrastructure.Idempotence;
    using UserService.Shared.Infrastructure.Persistence.Core.UnitOfWork.Behaviors;

    public class MediatrServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg =>
                {
                    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
                    cfg.AddOpenBehavior(typeof(UnitOfWorkBehavior<,>));
                });

            services.Decorate(typeof(INotificationHandler<>), typeof(IdempotentDomainEventHandler<>));
        }
    }
}