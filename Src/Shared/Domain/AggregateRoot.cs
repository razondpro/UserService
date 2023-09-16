namespace UserService.Shared.Domain
{
    using UserService.Shared.Domain.Events;
    public class AggregateRoot : Entity
    {

        private readonly List<DomainEvent> _domainEvents = new();

        protected AggregateRoot(UniqueIdentity? id) : base(id)
        {
        }

        protected void AddDomainEvent(DomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public IReadOnlyCollection<DomainEvent> GetDomainEvents() => _domainEvents.ToList();

        public void ClearDomainEvents() => _domainEvents.Clear();
    }
}