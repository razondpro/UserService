namespace UserService.Tests.Modules.User.Domain.Entities
{
    using UserService.Modules.User.Domain.Entities;
    using UserService.Modules.User.Domain.ValueObjects;
    using Shared.Domain;
    using Xunit;
    using Moq;
    using Modules.User.Domain.Events;

    public class UserTests
    {

        [Theory]
        [ClassData(typeof(UserData))]
        public void Create_ValidUser_ReturnsUserInstance(UniqueIdentity id, Email email, Name name, UserName userName)
        {
            var user = User.Create(id, email, name, userName);

            Assert.NotNull(user);
            Assert.Equal(email, user.Email);
            Assert.Equal(name, user.Name);
            // Assert.Equal(lastName, user.LastName);
            Assert.Equal(userName, user.UserName);
        }

        /*
            Search how to mock static method to test this

            [Theory]
            [ClassData(typeof(UserData))]
            public void Create_ValidUser_WithDomainEvent(UniqueIdentity id, Email email, FirstName firstName, LastName lastName, UserName userName)
            {
                var userMock = new Mock<User>(null, email, firstName, lastName, userName);
                var user = userMock.Object;

                userMock.Verify(x => x.AddDomainEvent(It.IsAny<UserCreated>()), Times.Once);


            }
            */
    }

    public class UserData : TheoryData<UniqueIdentity, Email, Name, UserName>
    {
        public UserData()
        {
            var email = Email.Create("example@thery.com");
            var name = Name.Create("John", "Doe");
            var userName = UserName.Create("johndoe");
            var id = new UniqueIdentity(null);

            Add(id, email, name, userName);
        }

    }

}