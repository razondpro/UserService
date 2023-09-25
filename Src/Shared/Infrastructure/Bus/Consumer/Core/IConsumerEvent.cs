using MediatR;

namespace UserService.Shared.Infrastructure.Bus.Consumer.Core
{
    public interface IConsumerEvent : INotification
    {
        public Guid Id { get; }
        public int Version { get; }
        public string Type { get; }

    }
}