namespace UserService.Modules.User.Application.CreateUser
{
    using LanguageExt;
    using UserService.Shared.Application.Commands;

    public sealed record CreateUserCommand(
        string FirstName,
        string LastName,
        string Email,
        string UserName) : ICommand<Either<Exception, Unit>>;
}