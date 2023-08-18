namespace UserService.Modules.User.Application.CreateUser
{
    using Microsoft.AspNetCore.Http.HttpResults;
    using UserService.Modules.User.Application.Abstractions.Commands;

    public sealed record CreateUserCommand(
        string FirstName,
        string LastName,
        string Email,
        string UserName) : ICommand<Results<Ok, BadRequest<string>>>
    {

    }
}