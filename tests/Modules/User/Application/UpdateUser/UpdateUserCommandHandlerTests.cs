using Moq;
using UserService.Modules.User.Application.UpdateUser;
using UserService.Modules.User.Domain.Repositories;
using FluentAssertions;
using Xunit;

namespace UserService.Tests.Modules.User.Application.UpdateUser
{
    using UserService.Modules.User.Domain.Entities;
    using UserService.Modules.User.Domain.ValueObjects;

    public class UpdateUserCommandHandlerTests
    {
        private readonly Mock<IUserReadRepository> _userReadRepository;
        private readonly Mock<IUserWriteRepository> _userWriteRepository;

        public UpdateUserCommandHandlerTests()
        {
            _userReadRepository = new();
            _userWriteRepository = new();
        }

        [Fact]
        public async void Handle_Should_ReturnUserNotFoundError_WhenUserDoesNotExist()
        {
            // Arrange
            var cmd = new UpdateUserCommand("nonexistentUser", "John", "Doe");
            var handler = new UpdateUserCommandHandler(_userWriteRepository.Object, _userReadRepository.Object);
            _userReadRepository.Setup(x => x.Get(It.IsAny<UserName>())).ReturnsAsync(null as User);

            // Act
            var result = await handler.Handle(cmd, default);

            // Assert
            result.IsLeft.Should().BeTrue();
            result.IfLeft(error => error.Should().BeOfType<UserNotFoundError>());
            _userReadRepository.Verify(x => x.Get(It.IsAny<UserName>()), Times.Once);
        }

        [Theory]
        [ClassData(typeof(ExistingUserData))]
        public async void Handle_Should_UpdateUser_WhenUserExists(User existingUser)
        {
            // Arrange
            var cmd = new UpdateUserCommand("existingUser", "John", "Doe");
            var handler = new UpdateUserCommandHandler(_userWriteRepository.Object, _userReadRepository.Object);
            _userReadRepository.Setup(x => x.Get(It.IsAny<UserName>())).ReturnsAsync(existingUser);

            // Act
            var result = await handler.Handle(cmd, default);

            // Assert
            result.IsRight.Should().BeTrue();
            _userReadRepository.Verify(x => x.Get(It.IsAny<UserName>()), Times.Once);
            _userWriteRepository.Verify(x => x.Update(existingUser), Times.Once);
        }
        [Theory]
        [ClassData(typeof(ExistingUserData))]
        public async void Handle_Should_UpdateUserWithNonNullFirstNameAndLastName(User existingUser)
        {
            // Arrange
            var cmd = new UpdateUserCommand(existingUser.UserName.Value, "Johnny", "Doey");
            var handler = new UpdateUserCommandHandler(_userWriteRepository.Object, _userReadRepository.Object);
            _userReadRepository.Setup(x => x.Get(It.IsAny<UserName>())).ReturnsAsync(existingUser);

            // Act
            var result = await handler.Handle(cmd, default);

            // Assert
            result.IsRight.Should().BeTrue();
            existingUser.Name.FirstName.Should().Be("Johnny");
            existingUser.Name.LastName.Should().Be("Doey");
        }

        [Theory]
        [ClassData(typeof(ExistingUserData))]
        public async void Handle_Should_UpdateUserWithNonNullFirstName(User existingUser)
        {
            // Arrange
            var cmd = new UpdateUserCommand(existingUser.UserName.Value, "Johnny", null);
            var handler = new UpdateUserCommandHandler(_userWriteRepository.Object, _userReadRepository.Object);
            _userReadRepository.Setup(x => x.Get(It.IsAny<UserName>())).ReturnsAsync(existingUser);

            // Act
            var result = await handler.Handle(cmd, default);

            // Assert
            result.IsRight.Should().BeTrue();
            existingUser.Name.FirstName.Should().Be("Johnny");
            existingUser.Name.LastName.Should().Be("Doe"); // LastName remains unchanged
        }

        [Theory]
        [ClassData(typeof(ExistingUserData))]
        public async void Handle_Should_UpdateUserWithNonNullLastName(User existingUser)
        {
            // Arrange
            var cmd = new UpdateUserCommand(existingUser.UserName.Value, null, "Doey");
            var handler = new UpdateUserCommandHandler(_userWriteRepository.Object, _userReadRepository.Object);
            _userReadRepository.Setup(x => x.Get(It.IsAny<UserName>())).ReturnsAsync(existingUser);

            // Act
            var result = await handler.Handle(cmd, default);

            // Assert
            result.IsRight.Should().BeTrue();
            existingUser.Name.FirstName.Should().Be("John"); // FirstName remains unchanged
            existingUser.Name.LastName.Should().Be("Doey");
        }

    }

    public class ExistingUserData : TheoryData<User>
    {
        public ExistingUserData()
        {
            var email = Email.Create("valid@email.com");
            var name = Name.Create("John", "Doe");
            var userName = UserName.Create("existingUser");

            Add(User.Create(null, email, name, userName));
        }
    }
}
