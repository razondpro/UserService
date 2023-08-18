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
        public string FirstName { get; init; }
        public string LastName { get; init; }

        private Name(string firstName, string? lastName)
        {
            FirstName = firstName;
            LastName = lastName ?? string.Empty;
        }

        public static Name Create(string firstName, string? lastName = null)
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

            if (lastName is not null)
            {
                if (lastName.Length < MinLength || lastName.Length > MaxLength)
                {
                    throw new InvalidNameException($"Last name must be between {MinLength} and {MaxLength} characters long");
                }

                if (!NameRegex.IsMatch(lastName))
                {
                    throw new InvalidNameException("Last name must contain only letters");
                }
            }

            return new(firstName, lastName);
        }
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return FirstName;
            yield return LastName;
        }
    }
}