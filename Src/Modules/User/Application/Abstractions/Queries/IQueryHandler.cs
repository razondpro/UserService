namespace UserService.Modules.User.Application.Abstractions.Queries
{
    using MediatR;
    public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
    {

    }
}