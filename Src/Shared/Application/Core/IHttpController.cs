namespace UserService.Shared.Application.Core
{
    public interface IHttpController<TRequest, TResult>
        where TResult : class, IResult
    {
        Task<TResult> Execute(TRequest request, CancellationToken cancellationToken = default);
    }
}