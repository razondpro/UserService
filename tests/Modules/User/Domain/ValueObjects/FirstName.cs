namespace UserService.Tests.Modules.User.Domain.ValueObjects
{
    using UserService.Modules.User.Domain.Exceptions;
    using UserService.Modules.User.Domain.ValueObjects;
    using Xunit;
    public class FirstNameTests
    {
        [Theory]
        [InlineData("John")]
        [InlineData("Mary")]
        [InlineData("Élodie")]
        public void Create_ValidFirstName_ReturnsFirstNameInstance(string firstName)
        {
            var result = FirstName.Create(firstName);
            Assert.NotNull(result);
            Assert.Equal(firstName, result.Value);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Create_NullOrWhiteSpaceFirstName_ThrowsInvalidNameException(string firstName)
        {
            var ex = Assert.Throws<InvalidNameException>(() => FirstName.Create(firstName));
            Assert.Equal("Name is required", ex.Message);
        }

        [Theory]
        [InlineData("Aa")]
        [InlineData("AbcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVW")]
        public void Create_ValidLengthFirstName_ReturnsFirstNameInstance(string firstName)
        {
            var result = FirstName.Create(firstName);
            Assert.NotNull(result);
            Assert.Equal(firstName, result.Value);
        }

        [Theory]
        [InlineData("A")]
        [InlineData("AbcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZA")]
        public void Create_InvalidLengthFirstName_ThrowsInvalidNameException(string firstName)
        {
            var ex = Assert.Throws<InvalidNameException>(() => FirstName.Create(firstName));
            Assert.Equal($"Name must be between {FirstName.MinLength} and {FirstName.MaxLength} characters long", ex.Message);
        }

        [Theory]
        [InlineData("John123")]
        [InlineData("Mary-Sue")]
        [InlineData("Élodie!")]
        public void Create_InvalidCharactersInFirstName_ThrowsInvalidNameException(string firstName)
        {
            var ex = Assert.Throws<InvalidNameException>(() => FirstName.Create(firstName));
            Assert.Equal("Name must contain only letters", ex.Message);
        }
    }
}