namespace UserService.Shared.Infrastructure.Bus.Kafka.Producer.Handlers
{
    using Events.Users;
    using Serilog;
    using UserService.Modules.User.Domain.Events.UserCreated;
    using UserService.Shared.Domain.Events;
    using UserService.Shared.Infrastructure.Bus.Kafka.Producer;

    public sealed class UserCreatedDomainEventHandler : IDomainEventHandler<UserCreatedDomainEvent>
    {
        private readonly KafkaProducer _kafkaProducer;
        public UserCreatedDomainEventHandler(KafkaProducer kafkaProducer)
        {
            _kafkaProducer = kafkaProducer;
        }
        public async Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            Log.Information("UserCreatedDomainEventHandler: {@notification}", notification);
            var message = new UserCreated
            {
                Email = notification.Email,
                UserName = notification.UserName
            };
            //we use the aggregateId as the key for the message, so that all events for a given aggregate are sent to the same partition
            await _kafkaProducer.ProduceAsync(notification.AggregateId.Value.ToString(), message);
        }
    }
}