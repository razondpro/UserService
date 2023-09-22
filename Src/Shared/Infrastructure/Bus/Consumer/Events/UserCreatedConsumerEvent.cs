using UserService.Shared.Infrastructure.Bus.Consumer.Core;

namespace UserService.Shared.Infrastructure.Bus.Consumer.Events
{
    public class UserCreatedConsumerEvent : BaseConsumerEvent
    {

        public const string EVENT_NAME = "user.created";

        public string Email { get; set; } = String.Empty;
        public string UserName { get; set; } = String.Empty;
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;

        public UserCreatedConsumerEvent() : base(EVENT_NAME) { }
    }
}