namespace UserService.Modules.User.Application.CreateUser
{
    using Microsoft.AspNetCore.Http.HttpResults;
    using UserService.Shared.Application.Commands;

    public sealed record CreateUserCommand(
        string FirstName,
        string LastName,
        string Email,
        string UserName) : ICommand<Results<Created, BadRequest<string>>>
    {

    }
}