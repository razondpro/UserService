namespace UserService.Modules.User.Domain.Events.UserCreated
{
    using Shared.Domain;
    using UserService.Shared.Domain.Events;

    public sealed record UserCreatedDomainEvent(
        UniqueIdentity AggregateId,
        string Email,
        string UserName
        ) : DomainEvent(new UniqueIdentity(null), DateTime.UtcNow, AggregateId)
    {

    }
}