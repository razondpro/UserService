namespace UserService.Shared.Application.Commands
{
    using MediatR;

    public interface ICommand : IRequest
    {

    }
    public interface ICommand<TResponse> : IRequest<TResponse>
    {

    }
}
