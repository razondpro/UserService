using UserService.Shared.Domain.Events;

namespace UserService.Shared.Domain
{
    public class AggregateRoot : Entity
    {

        private readonly List<IEvent> _domainEvents = new();

        protected AggregateRoot(UniqueIdentity? id) : base(id)
        {
        }

        protected void AddDomainEvent(IEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public IReadOnlyCollection<IEvent> GetDomainEvents() => _domainEvents.ToList();

        public void ClearDomainEvents() => _domainEvents.Clear();
    }
}