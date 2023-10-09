using Moq;
using UserService.Modules.User.Application.CreateUser;
using UserService.Modules.User.Domain.Repositories;
using FluentAssertions;
using Xunit;
using UserService.Modules.User.Domain.ValueObjects;

namespace UserService.Tests.Modules.User.Application.CreateUser
{
    using UserService.Modules.User.Domain.Entities;
    public class CreateuserCommandHandlerTests
    {

        private readonly Mock<IUserReadRepository> _userReadRepository;
        private readonly Mock<IUserWriteRepository> _userWriteRepository;

        public CreateuserCommandHandlerTests()
        {
            _userReadRepository = new();
            _userWriteRepository = new();
        }

        [Theory]
        [ClassData(typeof(NewUserData))]
        public async void Handle_Should_ReturnFaliureResult_WhenEmailNotUnique(User user)
        {
            var cmd = new CreateUserCommand(user.Name.FirstName, user.Name.LastName, user.Email.Value, user.UserName.Value);
            var handler = new CreateUserCommandHandler(_userWriteRepository.Object, _userReadRepository.Object);
            _userReadRepository.Setup(x => x.Get(It.IsAny<Email>())).ReturnsAsync(user);

            var result = await handler.Handle(cmd, default);

            result.IsLeft.Should().BeTrue();
            result.IfLeft(error => error.Should().BeOfType<EmailAlreadyExistsError>());
            _userReadRepository.Verify(x => x.Get(It.IsAny<Email>()), Times.Once);
        }

        [Theory]
        [ClassData(typeof(NewUserData))]
        public async void Handle_Should_ReturnFaliureResult_WhenUserNameNotUnique(User user)
        {
            var cmd = new CreateUserCommand(user.Name.FirstName, user.Name.LastName, user.Email.Value, user.UserName.Value);
            var handler = new CreateUserCommandHandler(_userWriteRepository.Object, _userReadRepository.Object);
            _userReadRepository.Setup(x => x.Get(It.IsAny<Email>())).ReturnsAsync(null as User);
            _userReadRepository.Setup(x => x.Get(It.IsAny<UserName>())).ReturnsAsync(user);

            var result = await handler.Handle(cmd, default);

            result.IsLeft.Should().BeTrue();
            result.IfLeft(error => error.Should().BeOfType<UserNameAlreadyExistsError>());
            _userReadRepository.Verify(x => x.Get(It.IsAny<Email>()), Times.Once);
            _userReadRepository.Verify(x => x.Get(It.IsAny<UserName>()), Times.Once);
        }

        [Theory]
        [ClassData(typeof(NewUserData))]
        public async void Handle_Should_ReturnSuccessResult_WhenUserCreated(User user)
        {
            var cmd = new CreateUserCommand(user.Name.FirstName, user.Name.LastName, user.Email.Value, user.UserName.Value);
            var handler = new CreateUserCommandHandler(_userWriteRepository.Object, _userReadRepository.Object);
            _userReadRepository.Setup(x => x.Get(It.IsAny<Email>())).ReturnsAsync(null as User);
            _userReadRepository.Setup(x => x.Get(It.IsAny<UserName>())).ReturnsAsync(null as User);

            var result = await handler.Handle(cmd, default);

            result.IsRight.Should().BeTrue();
            _userReadRepository.Verify(x => x.Get(It.IsAny<Email>()), Times.Once);
            _userReadRepository.Verify(x => x.Get(It.IsAny<UserName>()), Times.Once);
            _userWriteRepository.Verify(x => x.Create(It.IsAny<User>()), Times.Once);
        }
    }

    public class NewUserData : TheoryData<User>
    {
        public NewUserData()
        {
            var email = Email.Create("valid@email.com");
            var name = Name.Create("John", "Doe");
            var userName = UserName.Create("jd1");

            Add(User.Create(null, email, name, userName));
        }
    }

}