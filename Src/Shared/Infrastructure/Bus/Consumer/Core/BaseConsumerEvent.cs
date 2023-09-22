namespace UserService.Shared.Infrastructure.Bus.Consumer.Core
{
    public abstract class BaseConsumerEvent
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public string Type { get; set; }

        protected BaseConsumerEvent(string type)
        {
            Type = type;
        }

    }
}