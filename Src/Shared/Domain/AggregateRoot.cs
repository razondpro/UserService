using UserService.Shared.Domain.Events;

namespace UserService.Shared.Domain;

public class AggregateRoot : Entity
{

    private readonly List<IDomainEvent> domainEvents = new();

    protected AggregateRoot(UniqueIdentity? id) : base(id)
    {
    }

    public IEnumerable<IDomainEvent> GetDomainEvents()
    {
        return domainEvents;
    }

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        domainEvents.Add(domainEvent);
        DomainEvent.MarkAggregateForDispatch(this);
    }

    public void ClearEvents()
    {
        domainEvents.Clear();
    }

}