using Confluent.Kafka;
using LanguageExt.Pretty;
using Microsoft.Extensions.Options;
using Serilog;
using UserService.Shared.Infrastructure.Bus.Consumer;
using UserService.Shared.Infrastructure.Bus.Consumer.Core;

namespace UserService.Config.Bus
{
    public class BusServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureOptions<BusOptionsSetup>();
            services.AddSingleton<ConsumerConfig>(provider =>
            {
                var busOptions = provider.GetRequiredService<IOptions<BusOptions>>().Value;
                return new ConsumerConfig
                {
                    BootstrapServers = busOptions.BootstrapServers,
                    GroupId = busOptions.GroupId,
                    AutoOffsetReset = AutoOffsetReset.Earliest,
                    EnableAutoCommit = busOptions.EnableAutoCommit
                };
            });
            services.AddSingleton<IBusConsumer, EventBusConsumer>();
            services.AddHostedService<ConsumerBackgroundService>();
        }
    }
}