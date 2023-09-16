namespace UserService.Shared.Domain.Events
{
    public record DomainEvent(
        UniqueIdentity Id,
        DateTime Timestamp,
        UniqueIdentity AggregateId
        ) : IDomainEvent;
}