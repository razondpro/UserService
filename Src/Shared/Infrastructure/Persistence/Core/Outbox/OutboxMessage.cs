namespace UserService.Shared.Infrastructure.Persistence.Core.Outbox
{
    public class OutboxMessage
    {
        public Guid Id { get; private set; }
        //Event type
        public string Type { get; set; }

        public string Data { get; set; }

        public DateTime OccurredOn { get; private set; }

        public DateTime? ProcessedOn { get; private set; }

        public string? Error { get; set; }

        public OutboxMessage(string type, string data)
        {
            Id = Guid.NewGuid();
            Type = type;
            Data = data;
            OccurredOn = DateTime.Now.ToUniversalTime();
        }

        public void MarkAsProcessed()
        {
            ProcessedOn = DateTime.Now.ToUniversalTime();
        }

    }
}