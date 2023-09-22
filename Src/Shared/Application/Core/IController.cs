namespace UserService.Shared.Application.Core
{
    public interface IController<TRequest, TResponse>
    {
        Task<TResponse> execute(TRequest request, CancellationToken cancellationToken = default);
    }
}