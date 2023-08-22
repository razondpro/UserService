namespace UserService.Modules.User.Domain.Events.UserCreated
{
    public record UserCreatedData(string Email, string FirstName, string LastName, string UserName);
}