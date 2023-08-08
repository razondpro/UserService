namespace UserService.Tests.Modules.User.Domain.Events
{
    using UserService.Modules.User.Domain.Exceptions;
    using UserService.Modules.User.Domain.ValueObjects;
    using Xunit;

    public class UserNameTests
    {
        [Theory]
        [InlineData("john123")]
        [InlineData("user1234")]
        public void Create_ValidUserName_ReturnsUserNameInstance(string userName)
        {
            var result = UserName.Create(userName);
            Assert.NotNull(result);
            Assert.Equal(userName, result.Value);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Create_NullOrWhiteSpaceUserName_ThrowsInvalidUserNameException(string userName)
        {
            var ex = Assert.Throws<InvalidUserNameException>(() => UserName.Create(userName));
            Assert.Equal("User name is required", ex.Message);
        }

        [Theory]
        [InlineData("ab")]
        [InlineData("usernameusernameusernameusernameusernameusernameusernameusername")]
        public void Create_InvalidLengthUserName_ThrowsInvalidUserNameException(string userName)
        {
            var ex = Assert.Throws<InvalidUserNameException>(() => UserName.Create(userName));
            Assert.Equal($"User name must be between {UserName.MinLength} and {UserName.MaxLength} characters long", ex.Message);
        }

        [Theory]
        [InlineData("user@123")]
        [InlineData("username!")]
        public void Create_InvalidCharactersInUserName_ThrowsInvalidUserNameException(string userName)
        {
            var ex = Assert.Throws<InvalidUserNameException>(() => UserName.Create(userName));
            Assert.Equal("User name must contain only letters and numbers", ex.Message);
        }
    }
}