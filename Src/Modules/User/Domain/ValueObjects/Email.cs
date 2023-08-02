using System.Text.RegularExpressions;
using UserService.Modules.User.Domain.Exceptions;
using UserService.Shared.Domain;

namespace UserService.Modules.User.Domain.Entities;

public sealed class Email : ValueObject
{
    private static readonly Regex EmailRegex = new(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", RegexOptions.Compiled);
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