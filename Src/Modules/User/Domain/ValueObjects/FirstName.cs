using System.Text.RegularExpressions;
using UserService.Modules.User.Domain.Exceptions;
using UserService.Shared.Domain;

namespace UserService.Modules.User.Domain.ValueObjects;

public sealed class FirstName : ValueObject
{
    private static readonly int MaxLength = 50;
    private static readonly int MinLength = 2;
    private static readonly Regex NameRegex = new(@"^[a-zA-ZñÑáéíóúÁÉÍÓÚ]+$", RegexOptions.Compiled);
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

        if (firstName.Length <= MinLength || firstName.Length > MaxLength)
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