namespace UserService.Modules.User.Application.FindUserByEmail
{
    using LanguageExt;
    using UserService.Modules.User.Domain.Entities;
    using UserService.Shared.Application.Queries;

    public sealed record FindUserByEmailQuery(string Email) : IQuery<Either<Exception, User>>;

}