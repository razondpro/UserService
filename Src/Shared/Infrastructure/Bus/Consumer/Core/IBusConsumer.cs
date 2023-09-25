namespace UserService.Shared.Infrastructure.Bus.Consumer.Core
{
    public interface IBusConsumer
    {
        Task Consume(string[] topics, CancellationToken stoppingToken);
    }
}