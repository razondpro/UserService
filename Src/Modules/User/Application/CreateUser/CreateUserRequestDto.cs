namespace UserService.Modules.User.Application.CreateUser
{
    public sealed record CreateUserRequestDto(
        string FirstName,
        string LastName,
        string Email,
        string UserName
    );
}