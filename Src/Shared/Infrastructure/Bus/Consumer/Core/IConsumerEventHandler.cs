namespace UserService.Shared.Infrastructure.Bus.Consumer.Core
{
    public interface IConsumerEventHandler<T>
    {
        Task Handle(T @event, Func<Task> commit);
    }
}