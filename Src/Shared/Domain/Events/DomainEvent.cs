namespace UserService.Shared.Domain.Events
{
    using MediatR;

    public record DomainEvent(
        UniqueIdentity Id,
        DateTime Timestamp,
        UniqueIdentity AggregateId
        ) : INotification;
}