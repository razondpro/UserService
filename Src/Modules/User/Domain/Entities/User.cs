namespace UserService.Modules.User.Domain.Entities
{
    using Serilog;
    using Modules.User.Domain.Events.UserCreated;
    using Modules.User.Domain.ValueObjects;
    using Shared.Domain;
    using System;

    public class User : AggregateRoot, IAuditableEntity, IVersionedEntity
    {

        public Email Email { get; private set; }
        public Name Name { get; private set; }
        public UserName UserName { get; private set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int Version { get; set; }

        //Ef Core constructor
        private User() : base(null)
        {
            Email = null!;
            Name = null!;
            UserName = null!;
        }
        private User(UniqueIdentity? id, Email email, Name name, UserName userName) : base(id)
        {
            Email = email;
            Name = name;
            UserName = userName;
        }

        public static User Create(UniqueIdentity? id, Email email, Name name, UserName userName)
        {

            User user = new(id, email, name, userName);

            if (id is null)
            {
                user.AddDomainEvent(new UserCreatedDomainEvent(user.Id, user.Email.Value, user.UserName.Value));
                Log.Information("New User created: {@user}", user);
            }

            return user;
        }

    }
}