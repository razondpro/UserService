namespace UserService.Modules.User.Domain.Events.UserCreated
{
    using Shared.Domain;
    using UserService.Modules.User.Domain.ValueObjects;
    using UserService.Shared.Domain.Events;

    public record UserCreatedDomainEvent(
        UniqueIdentity AggregateId,
        String Email,
        String UserName
        ) : DomainEvent(new UniqueIdentity(null), DateTime.UtcNow, AggregateId)
    {

    }
}