using System.Text.RegularExpressions;
using UserService.Modules.User.Domain.Exceptions;
using UserService.Shared.Domain;

namespace UserService.Modules.User.Domain.ValueObjects;

public sealed class SurName : ValueObject
{
    private static readonly int MaxLength = 50;
    private static readonly int MinLength = 2;
    private static readonly Regex SurnameRegex = new(@"^[a-zA-ZñÑáéíóúÁÉÍÓÚ]+$", RegexOptions.Compiled);
    public string Value { get; }

    private SurName(string surname)
    {
        Value = surname;
    }

    public static SurName Create(string value)
    {

        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidNameException("Surname is required");
        }

        if (value.Length <= MinLength || value.Length > MaxLength)
        {
            throw new InvalidNameException($"Surname must be between {MinLength} and {MaxLength} characters long");
        }

        if (!SurnameRegex.IsMatch(value))
        {
            throw new InvalidNameException("Surname must contain only letters");
        }

        return new SurName(value);
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}