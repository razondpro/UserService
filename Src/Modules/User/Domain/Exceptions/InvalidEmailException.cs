namespace UserService.Modules.User.Domain.Exceptions;

public class InvalidEmailException : Exception
{
    public static readonly string DefaultMessage = "Email is invalid";
    public InvalidEmailException(string? message) : base(message ?? DefaultMessage)
    {
    }
}