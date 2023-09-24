namespace UserService.Shared.Infrastructure.Bus.Consumer.Core
{
    public interface IConsumerEvent
    {
        void Consume(string[] topics, CancellationToken stoppingToken);
    }
}