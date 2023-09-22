namespace UserService.Modules.User.Application.CreateUser
{
    public record CreateUserRequestDto(
        string FirstName,
        string LastName,
        string Email,
        string UserName
    );
}