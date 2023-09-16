namespace UserService.Shared.Domain.Events
{
    using MediatR;
    public interface IDomainEvent : INotification
    {
        UniqueIdentity Id { get; }
        DateTime Timestamp { get; }
        UniqueIdentity AggregateId { get; }

    }
}