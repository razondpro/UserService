using MediatR;

namespace UserService.Modules.User.Application.Abstractions.Queries
{
    public interface IQueryHandler<TQuery, TResponse>: IRequestHandler<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
    {

    }
}