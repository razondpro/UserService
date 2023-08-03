namespace UserService.Tests.Modules.User.Domain.ValueObjects;

using UserService.Modules.User.Domain.Exceptions;
using UserService.Modules.User.Domain.ValueObjects;
using Xunit;
public class LastNameTests
{
    [Theory]
    [InlineData("Doe")]
    [InlineData("Smith")]
    [InlineData("Gómez")]
    public void Create_ValidLastName_ReturnsLastNameInstance(string lastName)
    {
        var result = LastName.Create(lastName);
        Assert.NotNull(result);
        Assert.Equal(lastName, result.Value);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Create_NullOrWhiteSpaceLastName_ThrowsInvalidNameException(string lastName)
    {
        var ex = Assert.Throws<InvalidNameException>(() => LastName.Create(lastName));
        Assert.Equal("LastName is required", ex.Message);
    }

    [Theory]
    [InlineData("D")]
    [InlineData("DoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoe")]
    public void Create_InvalidLengthLastName_ThrowsInvalidNameException(string lastName)
    {
        var ex = Assert.Throws<InvalidNameException>(() => LastName.Create(lastName));
        Assert.Equal($"LastName must be between {LastName.MinLength} and {LastName.MaxLength} characters long", ex.Message);
    }

    [Theory]
    [InlineData("Doe123")]
    [InlineData("Smith-Sue")]
    [InlineData("Gómez!")]
    public void Create_InvalidCharactersInLastName_ThrowsInvalidNameException(string lastName)
    {
        var ex = Assert.Throws<InvalidNameException>(() => LastName.Create(lastName));
        Assert.Equal("LastName must contain only letters", ex.Message);
    }
}