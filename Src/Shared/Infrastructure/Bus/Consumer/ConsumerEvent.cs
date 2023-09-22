namespace UserService.Shared.Infrastructure.Bus.Consumer
{
    using System.Text.Json;
    using Confluent.Kafka;
    using Microsoft.Extensions.Options;
    using Serilog;
    using UserService.Shared.Infrastructure.Bus.Consumer.Core;
    using UserService.Shared.Infrastructure.Bus.Mappers;

    public class ConsumerEvent : IConsumerEvent
    {
        private readonly ConsumerConfig _consumerConfig;

        public ConsumerEvent(IOptions<ConsumerConfig> consumerConfig)
        {
            _consumerConfig = consumerConfig.Value;
        }

        //TODO - Improve this Consume method to invoke handler method to async
        public void Consume(List<string> topics, CancellationToken stoppingToken)
        {
            using var consumer = new ConsumerBuilder<Ignore, string>(_consumerConfig).Build();
            consumer.Subscribe(topics);
            try
            {
                Log.Information("Consumer started");

                while (!stoppingToken.IsCancellationRequested)
                {
                    var consumeResult = consumer.Consume(stoppingToken);

                    if (consumeResult.IsPartitionEOF) continue;

                    try
                    {
                        var options = new JsonSerializerOptions { Converters = { new EventJsonMapper() } };
                        var @event = JsonSerializer.Deserialize<BaseConsumerEvent>(consumeResult.Message.Value, options);

                        if (@event is null)
                            throw new Exception($"Error deserializing consumer event from topic {consumeResult.Topic}");

                        var handlerType = typeof(IConsumerEventHandler<>).MakeGenericType(@event.GetType());
                        var handler = Activator.CreateInstance(handlerType);
                        var method = handlerType.GetMethod("Handle");

                        if (method is null)
                            throw new Exception($"Error getting method Handle from consumer event handler {handlerType.Name}");

                        method.Invoke(handler, new object[] { @event, () => consumer.Commit(consumeResult) });
                    }
                    catch (Exception e)
                    {
                        Log.Error("Error consuming message: {@e}", e.Message);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                Log.Information("Consumer cancelled");
            }
            catch (Exception e)
            {
                Log.Error("Error consuming message: {@e}", e.Message);
            }
            finally
            {
                consumer.Close();
            }
        }
    }
}