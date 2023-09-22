namespace UserService.Shared.Infrastructure.Bus.Consumer.Core
{
    public interface IConsumerEvent
    {
        void Consume(List<string> topics, CancellationToken stoppingToken);
    }
}