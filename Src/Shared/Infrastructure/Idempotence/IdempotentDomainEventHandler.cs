namespace UserService.Shared.Infrastructure.Idempotence
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using UserService.Shared.Domain.Events;
    using UserService.Shared.Infrastructure.Persistence;
    using UserService.Shared.Infrastructure.Persistence.Core.Outbox;

    public sealed class IdempotentDomainEventHandler<T> : IDomainEventHandler<T>
        where T : DomainEvent
    {
        private readonly INotificationHandler<T> _handler;
        private readonly Database _database;


        public IdempotentDomainEventHandler(INotificationHandler<T> handler, Database database)
        {
            _handler = handler;
            _database = database;
        }
        public async Task Handle(T notification, CancellationToken cancellationToken)
        {
            var anyRecordOfConsumer = await _database.OutboxMessagesConsumer.AnyAsync(
                obc => obc.EventId == notification.Id.Value && obc.EventType == _handler.GetType().Name,
                cancellationToken
            );

            if (anyRecordOfConsumer)
            {
                return;
            }

            await _handler.Handle(notification, cancellationToken);

            await _database.OutboxMessagesConsumer.AddAsync(
                new OutboxMessageConsumer
                {
                    Id = Guid.NewGuid(),
                    EventId = notification.Id.Value,
                    EventType = _handler.GetType().Name,
                    Timestamp = DateTime.UtcNow
                },
                cancellationToken
            );

            await _database.SaveChangesAsync(cancellationToken);
        }
    }
}
