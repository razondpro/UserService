using UserService.Shared.Infrastructure.Bus.Consumer.Core;

namespace UserService.Shared.Infrastructure.Bus.Consumer.Events
{
    public class IgnoreConsumerEvent : BaseConsumerEvent
    {
        public const string EVENT_NAME = "ignore";

        public IgnoreConsumerEvent() : base(EVENT_NAME) { }
    }
}