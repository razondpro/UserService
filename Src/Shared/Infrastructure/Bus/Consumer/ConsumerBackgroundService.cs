using UserService.Shared.Infrastructure.Bus.Consumer.Core;

namespace UserService.Shared.Infrastructure.Bus.Consumer
{
    public class ConsumerBackgroundService : BackgroundService
    {
        private readonly IConsumerEvent _consumerEvent;

        public ConsumerBackgroundService(IConsumerEvent consumerEvent)
        {
            _consumerEvent = consumerEvent;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _consumerEvent.Consume(new List<string> { "user" }, stoppingToken);
            return Task.CompletedTask;
        }
    }
}