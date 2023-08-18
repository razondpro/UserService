namespace UserService.Modules.User.Application.Abstractions.Commands
{
    using MediatR;

    public interface ICommand : IRequest
    {

    }
    public interface ICommand<TResponse> : IRequest<TResponse>
    {

    }
}
