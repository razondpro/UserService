namespace UserService.Shared.Infrastructure.Http.Core
{
    public interface IHttpErrorResponse : IHttpResponse
    {
        List<ErrorDetail>? Errors { get; init; }

    }
}