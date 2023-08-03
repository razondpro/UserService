using UserService.Modules.User.Domain.Exceptions;
using UserService.Modules.User.Domain.ValueObjects;
using Xunit;

namespace UserService.Tests.Modules.User.Domain.ValueObjects;

public class EmailTests
{
    [Fact]
    public void Create_ValidEmail_ReturnsEmailInstance()
    {
        string validEmail = "test@example.com";
        var email = Email.Create(validEmail);
        Assert.NotNull(email);
        Assert.Equal(validEmail, email.Value);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_NullOrEmptyEmail_ThrowsInvalidEmailException(string email)
    {
        var ex = Assert.Throws<InvalidEmailException>(() => Email.Create(email));
        Assert.Equal("Email is required", ex.Message);
    }

    [Theory]
    [InlineData("i@")]
    [InlineData("invalid.email@com")]
    [InlineData("invalid.email@.com")]
    [InlineData("invalid.email@com.")]
    public void Create_InvalidEmail_ThrowsInvalidEmailException(string invalidEmail)
    {
        var ex = Assert.Throws<InvalidEmailException>(() => Email.Create(invalidEmail));
        Assert.Equal("Email is invalid", ex.Message);
    }

    [Theory]
    [InlineData($"i@iiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiii.i")]
    public void Create_InValidEmailWithInvalidLength_ThrowsInvalidEmailException(string invalidEmail)
    {

        var ex = Assert.Throws<InvalidEmailException>(() => Email.Create(invalidEmail));
        Assert.Equal($"Email must be less than {Email.Max_Length} characters long", ex.Message);
    }
}
