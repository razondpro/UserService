namespace UserService.Shared.Infrastructure.Http.Core
{
    public interface IHttpResponse
    {
        string Title { get; init; }
        int Status { get; init; }
    }
}