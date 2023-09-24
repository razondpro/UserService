using Microsoft.Extensions.Options;
using UserService.Config.Bus;
using UserService.Shared.Infrastructure.Bus.Consumer.Core;

namespace UserService.Shared.Infrastructure.Bus.Consumer
{
    public class ConsumerBackgroundService : BackgroundService
    {
        private readonly IConsumerEvent _consumerEvent;
        private readonly BusOptions _busOptions;

        public ConsumerBackgroundService(IConsumerEvent consumerEvent, IOptions<BusOptions> busOptions)
        {
            _consumerEvent = consumerEvent;
            _busOptions = busOptions.Value;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _consumerEvent.Consume(_busOptions.Topics, stoppingToken);
            return Task.CompletedTask;
        }
    }
}