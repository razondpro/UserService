namespace UserService.Shared.Infrastructure.Bus.Consumer
{
    using System.Text.Json;
    using Confluent.Kafka;
    using MediatR;
    using Serilog;
    using UserService.Shared.Infrastructure.Bus.Consumer.Core;
    using UserService.Shared.Infrastructure.Bus.Consumer.Events;
    using UserService.Shared.Infrastructure.Bus.Mappers;

    public class EventBusConsumer : IBusConsumer
    {
        private readonly ConsumerConfig _consumerConfig;
        private readonly IPublisher _publisher;

        public EventBusConsumer(ConsumerConfig consumerConfig, IPublisher publisher)
        {
            _consumerConfig = consumerConfig;
            _publisher = publisher;
        }

        public async Task Consume(string[] topics, CancellationToken stoppingToken)
        {
            using var consumer = new ConsumerBuilder<Ignore, string>(_consumerConfig).Build();
            consumer.Subscribe(topics);
            try
            {
                Log.Information("Consumer started");

                while (!stoppingToken.IsCancellationRequested)
                {
                    var consumeResult = await Task.Run(() => consumer.Consume(stoppingToken));

                    if (consumeResult.IsPartitionEOF) continue;

                    try
                    {
                        var options = new JsonSerializerOptions { Converters = { new EventJsonMapper() } };
                        var @event = JsonSerializer.Deserialize<IConsumerEvent>(consumeResult.Message.Value, options);

                        if (@event is null)
                        {
                            throw new Exception($"Error deserializing consumer event from topic {consumeResult.Topic}");
                        }

                        if (@event is IgnoreConsumerEvent) continue;

                        await _publisher.Publish(@event, stoppingToken);

                        consumer.Commit(consumeResult);
                    }
                    catch (Exception e)
                    {
                        Log.Error("Error consuming message: {@e}", e);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                Log.Information("Consumer cancelled");
            }
            catch (Exception e)
            {
                Log.Error("Error consuming message: {@e}", e);
            }
            finally
            {
                Log.Information("Consumer closing");
                consumer.Close();
            }
        }
    }
}