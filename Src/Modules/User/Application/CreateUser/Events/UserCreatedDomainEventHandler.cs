namespace UserService.Modules.User.Application.CreateUser.Events
{
    using MediatR;
    using Serilog;
    using UserService.Modules.User.Domain.Events.UserCreated;

    public sealed class UserCreatedDomainEventHandler : INotificationHandler<UserCreatedDomainEvent>
    {
        public Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}