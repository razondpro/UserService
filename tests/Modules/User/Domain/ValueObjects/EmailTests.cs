namespace UserService.Tests.Modules.User.Domain.ValueObjects
{
    using UserService.Modules.User.Domain.Exceptions;
    using UserService.Modules.User.Domain.ValueObjects;
    using FluentAssertions;
    using Xunit;
    public class EmailTests
    {
        [Fact]
        public void Create_ValidEmail_ReturnsEmailInstance()
        {
            string validEmail = "test@example.com";
            var email = Email.Create(validEmail);
            email.Should().NotBeNull();
            email.Should().BeOfType<Email>();
            email.Value.Should().Be(validEmail);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Create_NullOrEmptyEmail_ThrowsInvalidEmailException(string email)
        {
            var ex = Assert.Throws<InvalidEmailException>(() => Email.Create(email));
            ex.Message.Should().Be("Email is required");
        }

        [Theory]
        [InlineData("i@")]
        [InlineData("invalid.email@com")]
        [InlineData("invalid.email@.com")]
        [InlineData("invalid.email@com.")]
        public void Create_InvalidEmail_ThrowsInvalidEmailException(string invalidEmail)
        {
            var ex = Assert.Throws<InvalidEmailException>(() => Email.Create(invalidEmail));
            ex.Message.Should().Be("Email is invalid");
        }

        [Theory]
        [InlineData($"abcdefghijklmnopqrstuvwxyz1234567890abcdefghijklmnopqrstuvwxyz1234567890abcdefghijklmnopqrstuvwxyz1234567890abcdefghijklmnopqrstuvwxyz@example.com")]
        public void Create_InValidEmailWithInvalidLength_ThrowsInvalidEmailException(string invalidEmail)
        {

            var ex = Assert.Throws<InvalidEmailException>(() => Email.Create(invalidEmail));
            ex.Message.Should().Be($"Email must be less than {Email.MaxLength} characters long");
        }
    }
}