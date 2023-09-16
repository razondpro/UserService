namespace UserService.Shared.Infrastructure.Jobs.OutboxMessageProcessing
{
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using Polly;
    using Polly.Retry;
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
                            $"Error deserializing domain event from outbox message id {message.Id}");
                    }

                    AsyncRetryPolicy policy = Policy.Handle<Exception>()
                        .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                            (exception, timeSpan, retryCount, context) =>
                            {
                                Log.Error(exception.Message);
                            });

                    PolicyResult policyResult = await policy.ExecuteAndCaptureAsync(async () =>
                    {
                        await _publisher.Publish(domainEvent, context.CancellationToken);
                    });

                    if (policyResult.Outcome == OutcomeType.Failure)
                    {
                        throw policyResult.FinalException;
                    }

                }
                catch (Exception exception)
                {
                    Log.Error(exception.Message);
                    message.Error = exception.Message;
                }
                finally
                {
                    message.MarkAsProcessed();
                }

                _database.OutboxMessages.Update(message);
            }

            await _database.SaveChangesAsync(context.CancellationToken);
        }
    }
}