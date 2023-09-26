using LanguageExt;
using UserService.Shared.Application.Commands;

namespace UserService.Modules.User.Application.UpdateUser
{
    public sealed record UpdateUserCommand(
        string UserName,
        string? FirstName,
        string? LastName
    ) : ICommand<Either<Exception, Unit>>;
}