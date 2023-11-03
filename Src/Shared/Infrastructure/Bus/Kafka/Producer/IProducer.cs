namespace UserService.Shared.Infrastructure.Bus.Kafka.Producer
{
    public interface IProducer
    {
        public Task ProduceAsync<T>(string key, T message) where T : Avro.Specific.ISpecificRecord;
    }
}