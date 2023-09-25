using Microsoft.Extensions.Options;
using UserService.Config.Bus;
using UserService.Shared.Infrastructure.Bus.Consumer.Core;

namespace UserService.Shared.Infrastructure.Bus.Consumer
{
    public class ConsumerBackgroundService : BackgroundService
    {
        private readonly IBusConsumer _consumerEvent;
        private readonly BusOptions _busOptions;

        public ConsumerBackgroundService(IBusConsumer consumerEvent, IOptions<BusOptions> busOptions)
        {
            _consumerEvent = consumerEvent;
            _busOptions = busOptions.Value;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _consumerEvent.Consume(_busOptions.Topics, stoppingToken);
        }
    }
}