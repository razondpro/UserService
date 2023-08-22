namespace UserService.Shared.Domain.Events
{
    public class OutboxMessage
    {
        public Guid Id { get; private set; }

        public string Type { get; private set; }

        public string Data { get; private set; }

        public DateTime OccurredOn { get; private set; }

        public DateTime? ProcessedOn { get; private set; }

        public string? Error { get; private set; }

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