using System.Text.RegularExpressions;
using UserService.Modules.User.Domain.Exceptions;
using UserService.Shared.Domain;

namespace UserService.Modules.User.Domain.ValueObjects;

public sealed class Email : ValueObject
{
    public static readonly Regex EmailRegex = new(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", RegexOptions.Compiled);
    public static readonly int Max_Length = 100;
    public string Value { get; }

    private Email(string email)
    {
        Value = email;
    }

    public static Email Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidEmailException("Email is required");
        }

        if (value.Length > Max_Length)
        {
            throw new InvalidEmailException($"Email must be less than {Max_Length} characters long");
        }

        if (!EmailRegex.IsMatch(value))
        {
            throw new InvalidEmailException("Email is invalid");
        }

        return new Email(value);
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}