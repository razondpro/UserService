using MediatR;
using UserService.Shared.Infrastructure.Bus.Consumer.Core;
using UserService.Shared.Infrastructure.Bus.Consumer.Events;

namespace UserService.Modules.User.Application.CreateUser.Events
{
    public class UserCreatedConsumerEventHandler : IConsumerEventHandler<UserCreatedConsumerEvent>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public UserCreatedConsumerEventHandler(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task Handle(UserCreatedConsumerEvent notification, CancellationToken cancellationToken)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                var cmd = new CreateUserCommand(@notification.FirstName, @notification.LastName, @notification.Email, @notification.UserName);
                var result = await mediator.Send(cmd);
            }
        }
    }
}