namespace UserService.Modules.User.Domain.Events
{
    using Shared.Domain;
    using Shared.Domain.Events;
    using Modules.User.Domain.Entities;

    public sealed class UserCreated : IDomainEvent
    {
        public DateTime Timestamp { get; private set; }

        public UniqueIdentity AggregateId { get; private set; }

        public object Data { get; private set; }

        public UserCreated(User user)
        {
            Timestamp = DateTime.Now;
            AggregateId = user.Id;
            Data = new
            {
                user.Email,
                user.FirstName,
                user.LastName,
                user.UserName
            };
        }
    }
}