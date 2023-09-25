namespace UserService.Shared.Infrastructure.Bus.Consumer.Core
{
    public abstract class BaseConsumerEvent : IConsumerEvent
    {
        public Guid Id { get; }
        public int Version { get; }
        public string Type { get; }

        public BaseConsumerEvent(string type)
        {
            Type = type;
        }
    }
}