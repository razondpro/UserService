namespace UserService.Modules.User.Application.UpdateUser
{
    public sealed record UpdateUserRequestDto(
        string UserName,
        string? FirstName,
        string? LastName
    );
}