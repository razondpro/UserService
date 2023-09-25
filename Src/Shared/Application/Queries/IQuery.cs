namespace UserService.Shared.Application.Queries
{
    using MediatR;

    public interface IQuery<TResponse> : IRequest<TResponse>
    {

    }
}