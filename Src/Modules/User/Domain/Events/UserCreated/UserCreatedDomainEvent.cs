namespace UserService.Modules.User.Domain.Events.UserCreated
{
    using Shared.Domain;
    using Modules.User.Domain.Entities;
    using UserService.Shared.Domain.Events;

    public sealed class UserCreatedDomainEvent : IDomainEvent<UserCreatedData>
    {
        public UniqueIdentity Id { get; private set; }
        public DateTime Timestamp { get; private set; }

        public UniqueIdentity AggregateId { get; private set; }

        public UserCreatedData Data { get; private set; }

        public UserCreatedDomainEvent(User user)
        {
            Id = new UniqueIdentity(null);
            Timestamp = DateTime.Now;
            AggregateId = user.Id;
            Data = new UserCreatedData(
                user.Email.Value,
                user.Name.FirstName,
                user.Name.LastName,
                user.UserName.Value
            );
        }
    }
}