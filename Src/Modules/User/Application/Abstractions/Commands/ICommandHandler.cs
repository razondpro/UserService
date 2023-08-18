

namespace UserService.Modules.User.Application.Abstractions.Commands
{
    using MediatR;
    public interface ICommandHandler<TCommand> : IRequestHandler<TCommand>
          where TCommand : ICommand
    {
    }
    public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
          where TCommand : ICommand<TResponse>
    {
    }
}