namespace UserService.Modules.User.Application.GetUserByEmail
{
    using Microsoft.AspNetCore.Http.HttpResults;
    using UserService.Shared.Application.Queries;

    public sealed record GetUserByEmailQuery(string Email) : IQuery<Results<Ok<UserResponse>, NotFound>>;

    public sealed record UserResponse(
        string FirstName,
        string LastName,
        string Email,
        string UserName
    );
}