namespace UserService.Tests.Modules.User.Domain.Entities
{
    using UserService.Modules.User.Domain.Entities;
    using UserService.Modules.User.Domain.ValueObjects;
    using Shared.Domain;
    using Xunit;

    public class UserTests
    {

        [Theory]
        [ClassData(typeof(ExisitingUserData))]
        public void Create_ValidUser_ReturnsUserInstance(UniqueIdentity id, Email email, Name name, UserName userName)
        {
            var user = User.Create(id, email, name, userName);

            Assert.NotNull(user);
            Assert.Equal(email, user.Email);
            Assert.Equal(name, user.Name);
            Assert.Equal(userName, user.UserName);
        }

        [Theory]
        [ClassData(typeof(NewUserData))]
        public void Create_NewValidUser_WithDomainEvent(Email email, Name name, UserName userName)
        {
            var user = User.Create(null, email, name, userName);

            var domainEvents = user.GetDomainEvents();

            Assert.NotNull(domainEvents);
            Assert.Single(domainEvents);
        }

        [Theory]
        [ClassData(typeof(ExisitingUserData))]
        public void Create_ExistingValidUser_WithoutDomainEvent(UniqueIdentity id, Email email, Name name, UserName userName)
        {
            var user = User.Create(id, email, name, userName);

            var domainEvents = user.GetDomainEvents();

            Assert.NotNull(domainEvents);
            Assert.Empty(domainEvents);
        }
    }

    public class ExisitingUserData : TheoryData<UniqueIdentity, Email, Name, UserName>
    {
        public ExisitingUserData()
        {
            var email = Email.Create("example@thery.com");
            var name = Name.Create("John", "Doe");
            var userName = UserName.Create("johndoe");
            var id = new UniqueIdentity(null);

            Add(id, email, name, userName);
        }
    }

    public class NewUserData : TheoryData<Email, Name, UserName>
    {
        public NewUserData()
        {
            var email = Email.Create("example@thery.com");
            var name = Name.Create("John", "Doe");
            var userName = UserName.Create("johndoe");

            Add(email, name, userName);
        }
    }

}