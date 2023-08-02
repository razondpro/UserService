using UserService.Shared.Domain;
using UserService.Shared.Domain.Events;

namespace UserService.Modules.User.Domain.Events;

public sealed class UserCreated : IDomainEvent
{
    public DateTime Timestamp { get; private set; }

    public UniqueIdentity AggregateId { get; private set; }

    public object Data { get; private set; }

    public UserCreated(Entities.User user)
    {
        Timestamp = DateTime.Now;
        AggregateId = user.GetId();
        Data = new
        {
            user.Email,
            user.FirstName,
            user.LastName,
            user.UserName
        };
    }
}