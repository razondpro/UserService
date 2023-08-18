namespace UserService.Modules.User.Domain.ValueObjects
{
    using System.Text.RegularExpressions;
    using Modules.User.Domain.Exceptions;
    using Shared.Domain;

    public sealed class UserName : ValueObject
    {
        public static readonly int MaxLength = 20;
        public static readonly int MinLength = 3;
        public static readonly Regex UserNameRegex = new(@"^[a-zA-Z0-9]+$", RegexOptions.Compiled);
        private UserName(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static explicit operator string(UserName userName) => userName.Value;

        public static UserName Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidUserNameException("User name is required");
            }

            if (value.Length <= MinLength || value.Length > MaxLength)
            {
                throw new InvalidUserNameException($"User name must be between {MinLength} and {MaxLength} characters long");
            }

            if (!UserNameRegex.IsMatch(value))
            {
                throw new InvalidUserNameException("User name must contain only letters and numbers");
            }

            return new UserName(value);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}