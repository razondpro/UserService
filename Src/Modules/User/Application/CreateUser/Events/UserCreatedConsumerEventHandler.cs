using MediatR;
using UserService.Shared.Infrastructure.Bus.Consumer.Core;
using UserService.Shared.Infrastructure.Bus.Consumer.Events;

namespace UserService.Modules.User.Application.CreateUser.Events
{
    public class UserCreatedConsumerEventHandler : IConsumerEventHandler<UserCreatedConsumerEvent>
    {
        private readonly IMediator _mediator;

        public UserCreatedConsumerEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(UserCreatedConsumerEvent @event, Func<Task> commit)
        {
            var cmd = new CreateUserCommand(@event.FirstName, @event.LastName, @event.Email, @event.UserName);
            var results = await _mediator.Send(cmd);

            //if (results.IsOk) await commit();

        }
    }
}