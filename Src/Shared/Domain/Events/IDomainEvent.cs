namespace UserService.Shared.Domain.Events
{
    public interface IDomainEvent<T> : IEvent
    {
        UniqueIdentity AggregateId { get; }
        T Data { get; }
    }
}