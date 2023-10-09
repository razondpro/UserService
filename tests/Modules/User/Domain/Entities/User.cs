namespace UserService.Tests.Modules.User.Domain.Entities
{
    using UserService.Modules.User.Domain.Entities;
    using UserService.Modules.User.Domain.ValueObjects;
    using Shared.Domain;
    using FluentAssertions;
    using Xunit;
    using UserService.Shared.Domain.Events;

    public class UserTests
    {

        [Theory]
        [ClassData(typeof(ExisitingUserData))]
        public void Create_ValidUser_ReturnsUserInstance(UniqueIdentity id, Email email, Name name, UserName userName)
        {
            var user = User.Create(id, email, name, userName);

            user.Should().NotBeNull();
            user.Should().BeOfType<User>();
            user.Id.Value.Should().Be(id.Value);
            user.Email.Value.Should().Be(email.Value);
            user.Name.FirstName.Should().Be(name.FirstName);
            user.Name.LastName.Should().Be(name.LastName);
            user.UserName.Value.Should().Be(userName.Value);
        }

        [Theory]
        [ClassData(typeof(NewUserData))]
        public void Create_NewValidUser_WithDomainEvent(Email email, Name name, UserName userName)
        {
            var user = User.Create(null, email, name, userName);

            var domainEvents = user.GetDomainEvents();

            domainEvents.Should().NotBeNull();
            domainEvents.Should().NotBeEmpty();
            domainEvents.Should().ContainSingle();
        }

        [Theory]
        [ClassData(typeof(ExisitingUserData))]
        public void Create_ExistingValidUser_WithoutDomainEvent(UniqueIdentity id, Email email, Name name, UserName userName)
        {
            var user = User.Create(id, email, name, userName);

            var domainEvents = user.GetDomainEvents();

            domainEvents.Should().NotBeNull();
            domainEvents.Should().BeEmpty();
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