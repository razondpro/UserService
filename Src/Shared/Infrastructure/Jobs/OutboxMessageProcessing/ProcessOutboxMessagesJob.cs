namespace UserService.Shared.Infrastructure.Jobs.OutboxMessageProcessing
{
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using Quartz;
    using Serilog;
    using UserService.Shared.Domain.Events;
    using UserService.Shared.Infrastructure.Persistence;

    [DisallowConcurrentExecution]
    public class ProcessOutboxMessagesJob : IJob
    {
        private readonly Database _database;
        private readonly IPublisher _publisher;

        public ProcessOutboxMessagesJob(Database database, IPublisher publisher)
        {
            _database = database;
            _publisher = publisher;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            var obMessages = await _database.OutboxMessages
                .Where(m => m.ProcessedOn == null)
                .OrderBy(m => m.Id)
                .Take(10)
                .ToListAsync(context.CancellationToken);

            foreach (var message in obMessages)
            {
                try
                {
                    var domainEvent = JsonConvert.DeserializeObject<DomainEvent>(message.Data, new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All
                    });

                    if (domainEvent == null)
                    {
                        throw new Exception(
                            $"Error deserializing domain event from outbox message id {message.Id} with data {message.Data}");
                    }

                    await _publisher.Publish(domainEvent, context.CancellationToken);
                    message.MarkAsProcessed();
                }
                catch (Exception exception)
                {
                    Log.Error(exception.Message);
                    message.Error = exception.Message;
                }

                _database.OutboxMessages.Update(message);
            }

            await _database.SaveChangesAsync(context.CancellationToken);
        }
    }
}