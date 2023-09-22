namespace UserService.Modules.User.Application.FindUserByEmail
{
    using LanguageExt;
    using UserService.Shared.Application.Queries;

    public sealed record FindUserByEmailQuery(string Email) : IQuery<Either<Exception, UserResponse>>;

    public sealed record UserResponse(
        string FirstName,
        string LastName,
        string Email,
        string UserName
    );
}