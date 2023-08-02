using System.Text.RegularExpressions;
using UserService.Modules.User.Domain.Exceptions;
using UserService.Shared.Domain;

namespace UserService.Modules.User.Domain.ValueObjects;

public sealed class LastName : ValueObject
{
    private static readonly int MaxLength = 50;
    private static readonly int MinLength = 2;
    private static readonly Regex LastNameRegex = new(@"^[a-zA-ZñÑáéíóúÁÉÍÓÚ]+$", RegexOptions.Compiled);
    public string Value { get; }

    private LastName(string lastName)
    {
        Value = lastName;
    }

    public static LastName Create(string value)
    {

        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidNameException("LastName is required");
        }

        if (value.Length <= MinLength || value.Length > MaxLength)
        {
            throw new InvalidNameException($"LastName must be between {MinLength} and {MaxLength} characters long");
        }

        if (!LastNameRegex.IsMatch(value))
        {
            throw new InvalidNameException("LastName must contain only letters");
        }

        return new LastName(value);
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}