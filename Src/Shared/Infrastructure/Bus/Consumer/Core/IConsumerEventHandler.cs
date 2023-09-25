using MediatR;

namespace UserService.Shared.Infrastructure.Bus.Consumer.Core
{
    public interface IConsumerEventHandler<T> : INotificationHandler<T> where T : BaseConsumerEvent { }
}