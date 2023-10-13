namespace UserService.Shared.Infrastructure.Http.Core
{
    public class ApiHttpResponse : IHttpResponse
    {
        public string Title { get; init; }
        public int Status { get; init; }
        public ApiHttpResponse(string title, int status)
        {
            Title = title;
            Status = status;
        }
    }
}