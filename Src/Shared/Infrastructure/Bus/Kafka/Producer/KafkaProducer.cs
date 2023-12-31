using Avro.Specific;
using KafkaFlow;

namespace UserService.Shared.Infrastructure.Bus.Kafka.Producer
{
    public class KafkaProducer : IProducer
    {
        private readonly IMessageProducer<KafkaProducer> _producer;

        public KafkaProducer(IMessageProducer<KafkaProducer> producer)
        {
            _producer = producer;
        }

        public async Task ProduceAsync<T>(string key, T message) where T : ISpecificRecord
        {
            await _producer.ProduceAsync(key, message);
        }
    }
}