namespace UserService.Modules.User.Application.CreateUser.Events
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Serilog;
    using UserService.Modules.User.Domain.Events.UserCreated;
    using UserService.Shared.Domain.Events;

    public sealed class UserCreatedDomainEventHandler : IDomainEventHandler<UserCreatedDomainEvent>
    {
        public Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            Log.Information("UserCreatedDomainEventHandler: {@notification}", notification);
            return Task.CompletedTask;
        }
    }
}