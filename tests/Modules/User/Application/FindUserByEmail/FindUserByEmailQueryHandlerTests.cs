namespace UserService.Tests.Modules.User.Application.FindUserByEmail
{
    using FluentAssertions;
    using Moq;
    using UserService.Modules.User.Application.FindUserByEmail;
    using UserService.Modules.User.Application.UpdateUser;
    using UserService.Modules.User.Domain.Repositories;
    using UserService.Modules.User.Domain.Entities;
    using UserService.Modules.User.Domain.ValueObjects;
    using Xunit;
    public class FindUserByEmailQueryHandlerTests
    {

        private readonly Mock<IUserReadRepository> _userRepository;

        public FindUserByEmailQueryHandlerTests()
        {
            _userRepository = new();
        }

        [Fact]
        public async void Handle_Should_ReturnUserNotFoundError_WhenUserDoesNotExist()
        {
            // Arrange
            var cmd = new FindUserByEmailQuery("nonexistentUser@email.com");
            var handler = new FindUserByEmailQueryHandler(_userRepository.Object);
            _userRepository.Setup(x => x.Get(It.IsAny<Email>())).ReturnsAsync(null as User);

            // Act
            var result = await handler.Handle(cmd, default);

            // Assert
            result.IsLeft.Should().BeTrue();
            result.IfLeft(error => error.Should().BeOfType<UserNotFoundByEmailError>());
            _userRepository.Verify(x => x.Get(It.IsAny<Email>()), Times.Once);
        }

        [Fact]
        public async void Handle_Should_ReturnUser_WhenUserExists()
        {
            // Arrange
            var email = Email.Create("valid@email.com");
            var name = Name.Create("John", "Doe");
            var userName = UserName.Create("existingUser");
            var existingUser = User.Create(null, email, name, userName);

            var cmd = new FindUserByEmailQuery(existingUser.Email.Value);
            var handler = new FindUserByEmailQueryHandler(_userRepository.Object);
            _userRepository.Setup(x => x.Get(It.IsAny<Email>())).ReturnsAsync(existingUser);

            // Act
            var result = await handler.Handle(cmd, default);

            // Assert
            result.IsRight.Should().BeTrue();
            _userRepository.Verify(x => x.Get(It.IsAny<Email>()), Times.Once);
        }
    }
}