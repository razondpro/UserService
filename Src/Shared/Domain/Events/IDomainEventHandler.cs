using MediatR;

namespace UserService.Shared.Domain.Events
{
    public interface IDomainEventHandler<T> : INotificationHandler<T> where T : DomainEvent
    {

    }
}