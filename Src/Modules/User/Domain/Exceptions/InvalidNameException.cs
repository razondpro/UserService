namespace UserService.Modules.User.Domain.Exceptions;
public class InvalidNameException : Exception
{
    public static readonly string DefaultMessage = "Name is invalid";
    public InvalidNameException(string? message) : base(message ?? DefaultMessage)
    {
    }
}
