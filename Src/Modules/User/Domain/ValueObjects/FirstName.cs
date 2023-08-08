namespace UserService.Modules.User.Domain.ValueObjects
{
    using System.Text.RegularExpressions;
    using Modules.User.Domain.Exceptions;
    using Shared.Domain;

    public sealed class FirstName : ValueObject
    {
        public static readonly int MaxLength = 50;
        public static readonly int MinLength = 2;
        public static readonly Regex NameRegex = new(@"^[a-zA-ZñÑáéíóúÁÉÍÓÚ]+$", RegexOptions.Compiled);
        public string Value { get; init; }

        private FirstName(string firstName)
        {
            Value = firstName;
        }

        public static FirstName Create(string firstName)
        {

            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new InvalidNameException("Name is required");
            }

            if (firstName.Length < MinLength || firstName.Length > MaxLength)
            {
                throw new InvalidNameException($"Name must be between {MinLength} and {MaxLength} characters long");
            }

            if (!NameRegex.IsMatch(firstName))
            {
                throw new InvalidNameException("Name must contain only letters");
            }

            return new(firstName);
        }
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}