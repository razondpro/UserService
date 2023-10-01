namespace UserService.Shared.Infrastructure.Bus.Kafka.Producer
{
    public interface IProducer
    {
        public Task ProduceAsync<T>(T @event) where T : Avro.Specific.ISpecificRecord;
    }
}