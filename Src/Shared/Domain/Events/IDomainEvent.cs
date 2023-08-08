namespace UserService.Shared.Domain.Events
{
    public interface IDomainEvent
    {
        DateTime Timestamp { get; }
        UniqueIdentity AggregateId { get; }
        object Data { get; }
    }
}