namespace UserService.Shared.Infrastructure.Persistence.Core.Outbox
{
    public sealed class OutboxMessageConsumer
    {
        public Guid Id { get; set; }
        // Event type
        public string EventType { get; set; } = String.Empty;
        public Guid EventId { get; set; }
        public DateTime Timestamp { get; set; }

    }
}