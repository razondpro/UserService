namespace UserService.Modules.User.Domain.ValueObjects
{
    using System.Text.RegularExpressions;
    using Modules.User.Domain.Exceptions;
    using Shared.Domain;

    public sealed class Name : ValueObject
    {
        public static readonly int MaxLength = 50;
        public static readonly int MinLength = 2;
        public static readonly Regex NameRegex = new(@"^[a-zA-ZñÑáéíóúÁÉÍÓÚ]+$", RegexOptions.Compiled);
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        private Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public static Name Create(string firstName, string lastName)
        {

            ValidateFirstName(firstName);
            ValidateLastName(lastName);

            return new(firstName, lastName);
        }

        public void UpdateFirstName(string firstName)
        {
            ValidateFirstName(firstName);
            FirstName = firstName;
        }

        public void UpdateLastName(string lastName)
        {
            ValidateLastName(lastName);
            LastName = lastName;
        }

        private static void ValidateLastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new InvalidNameException("Last name is required");
            }

            if (lastName.Length < MinLength || lastName.Length > MaxLength)
            {
                throw new InvalidNameException($"Name must be between {MinLength} and {MaxLength} characters long");
            }

            if (!NameRegex.IsMatch(lastName))
            {
                throw new InvalidNameException("Name must contain only letters");
            }
        }

        private static void ValidateFirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new InvalidNameException("First name is required");
            }

            if (firstName.Length < MinLength || firstName.Length > MaxLength)
            {
                throw new InvalidNameException($"Name must be between {MinLength} and {MaxLength} characters long");
            }

            if (!NameRegex.IsMatch(firstName))
            {
                throw new InvalidNameException("Name must contain only letters");
            }
        }
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return FirstName;
            yield return LastName;
        }
    }
}