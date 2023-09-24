namespace UserService.Shared.Infrastructure.Http.Core
{
    public class ApiHttpResponse : IHttpResponse
    {
        public string Title { get; init; }
        public int Status { get; init; }

        public List<ErrorDetail>? Errors { get; init; }

        public ApiHttpResponse(string title, int status, List<ErrorDetail> errors)
        {
            Title = title;
            Status = status;
            Errors = errors;
        }
        public ApiHttpResponse(string title, int status)
        {
            Title = title;
            Status = status;
        }
    }

    public class ErrorDetail
    {
        public string PropertyName { get; set; }
        public string ErrorMessage { get; set; }

        public ErrorDetail(string propertyName, string errorMessage)
        {
            PropertyName = propertyName;
            ErrorMessage = errorMessage;
        }
    }
}