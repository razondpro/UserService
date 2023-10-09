using UserService.Modules.User.Domain.Exceptions;
using UserService.Modules.User.Domain.ValueObjects;
using Xunit;
using FluentAssertions;

namespace UserService.Tests.Modules.User.Domain.ValueObjects
{
    public class NameTests
    {
        [Fact]
        public void Create_ValidName_ReturnsNameInstance()
        {
            string validFirstName = "John";
            string validLastName = "Doe";
            var name = Name.Create(validFirstName, validLastName);
            name.Should().NotBeNull();
            name.Should().BeOfType<Name>();
            name.FirstName.Should().Be(validFirstName);
            name.LastName.Should().Be(validLastName);
        }

        [Theory]
        [InlineData(null, "Doe")]
        [InlineData("", "Doe")]
        [InlineData("   ", "Doe")]
        public void Create_NullOrEmptyNamePart_ThrowsInvalidNameException(string firstName, string lastName)
        {
            var ex = Assert.Throws<InvalidNameException>(() => Name.Create(firstName, lastName));
            ex.Message.Should().Be("First name is required");
        }

        [Theory]
        [InlineData("John", null)]
        [InlineData("John", "")]
        [InlineData("John", "   ")]
        public void Create_NullOrEmptyLastName_ThrowsInvalidNameException(string firstName, string lastName)
        {
            var ex = Assert.Throws<InvalidNameException>(() => Name.Create(firstName, lastName));
            ex.Message.Should().Be("Last name is required");
        }

        [Theory]
        [InlineData("J", "Doe")]
        [InlineData("John", "DoeJohnDoeJohnDoeJohnDoeJohnDoeJohnDoeJohnDoeJohnDoeJohnDoeJohnDoe")]
        public void Create_InvalidNameLengthOrFormat_ThrowsInvalidNameException(string firstName, string lastName)
        {
            var ex = Assert.Throws<InvalidNameException>(() => Name.Create(firstName, lastName));
            ex.Message.Should().Be($"Name must be between {Name.MinLength} and {Name.MaxLength} characters long");
        }

        [Theory]
        [InlineData("John", "Doe@123")]
        [InlineData("J@hn", "Doe")]
        public void Create_InvalidNameFormat_ThrowsInvalidNameException(string firstName, string lastName)
        {
            var ex = Assert.Throws<InvalidNameException>(() => Name.Create(firstName, lastName));
            ex.Message.Should().Be("Name must contain only letters");
        }

        [Fact]
        public void UpdateFirstName_ValidFirstName_UpdatesFirstName()
        {
            var name = Name.Create("John", "Doe");
            name.UpdateFirstName("Jane");
            name.FirstName.Should().Be("Jane");
        }

        [Fact]
        public void UpdateLastName_ValidLastName_UpdatesLastName()
        {
            var name = Name.Create("John", "Doe");
            name.UpdateLastName("Smith");
            name.LastName.Should().Be("Smith");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void UpdateFirstName_NullOrEmptyNamePart_ThrowsInvalidNameException(string firstName)
        {
            var name = Name.Create("John", "Doe");
            var ex = Assert.Throws<InvalidNameException>(() => name.UpdateFirstName(firstName));
            ex.Message.Should().Be("First name is required");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void UpdateLastName_NullOrEmptyNamePart_ThrowsInvalidNameException(string lastName)
        {
            var name = Name.Create("John", "Doe");
            var ex = Assert.Throws<InvalidNameException>(() => name.UpdateLastName(lastName));
            ex.Message.Should().Be("Last name is required");
        }

        [Theory]
        [InlineData("John1")]
        [InlineData("J@hn")]
        public void UpdateFirstName_InvalidNameFormat_ThrowsInvalidNameException(string firstName)
        {
            var name = Name.Create("John", "Doe");
            var ex = Assert.Throws<InvalidNameException>(() => name.UpdateFirstName(firstName));
            Assert.Equal("Name must contain only letters", ex.Message);
        }

        [Theory]
        [InlineData("Doe1")]
        [InlineData("D@e")]
        public void UpdateLastName_InvalidNameFormat_ThrowsInvalidNameException(string lastName)
        {
            var name = Name.Create("John", "Doe");
            var ex = Assert.Throws<InvalidNameException>(() => name.UpdateLastName(lastName));
            Assert.Equal("Name must contain only letters", ex.Message);
        }

        [Theory]
        [InlineData("J")]
        [InlineData("JohnJohnJohnJohnJohnJohnJohnJohnJohnJohnJohnJohnJohnJohnJohnJohnJohnJohnJohnJohn")]
        public void UpdateFirstName_InvalidNameLength_ThrowsInvalidNameException(string firstName)
        {
            var name = Name.Create("John", "Doe");
            var ex = Assert.Throws<InvalidNameException>(() => name.UpdateFirstName(firstName));
            ex.Message.Should().Be($"Name must be between {Name.MinLength} and {Name.MaxLength} characters long");
        }

        [Theory]
        [InlineData("D")]
        [InlineData("DoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoe")]
        public void UpdateLastName_InvalidNameLength_ThrowsInvalidNameException(string lastName)
        {
            var name = Name.Create("John", "Doe");
            var ex = Assert.Throws<InvalidNameException>(() => name.UpdateLastName(lastName));
            ex.Message.Should().Be($"Name must be between {Name.MinLength} and {Name.MaxLength} characters long");
        }

    }
}