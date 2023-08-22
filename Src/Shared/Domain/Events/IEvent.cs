namespace UserService.Shared.Domain.Events
{
    using MediatR;

    public interface IEvent : INotification
    {
        UniqueIdentity Id { get; }
        DateTime Timestamp { get; }
    }
}