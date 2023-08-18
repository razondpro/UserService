namespace UserService.Modules.User.Application.Abstractions.Queries
{
    using MediatR;

    public interface IQuery<TResponse> : IRequest<TResponse>
    {

    }
}