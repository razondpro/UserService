namespace UserService.Modules.User.Domain.Entities
{
    using Serilog;
    using Modules.User.Domain.Events;
    using Modules.User.Domain.ValueObjects;
    using Shared.Domain;

    public class User : AggregateRoot
    {

        public Email Email { get; private set; }
        public FirstName FirstName { get; private set; }
        public LastName LastName { get; private set; }
        public UserName UserName { get; private set; }

        private User(UniqueIdentity? id, Email email, FirstName firstName, LastName lastName, UserName userName) : base(id)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
        }

        public static User Create(UniqueIdentity? id, Email email, FirstName firstName, LastName lastName, UserName userName)
        {

            User user = new(id, email, firstName, lastName, userName);

            if (id is null)
            {
                user.AddDomainEvent(new UserCreated(user));
                Log.Information("New User created: {@user}", user);
            }

            return user;
        }

    }
}