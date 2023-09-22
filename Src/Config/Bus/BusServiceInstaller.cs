using UserService.Shared.Infrastructure.Bus.Consumer;
using UserService.Shared.Infrastructure.Bus.Consumer.Core;

namespace UserService.Config.Bus
{
    public class BusServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddHostedService<ConsumerBackgroundService>();
            services.AddSingleton<IConsumerEvent, ConsumerEvent>();
        }
    }
}