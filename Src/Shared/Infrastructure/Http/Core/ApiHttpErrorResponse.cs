namespace UserService.Shared.Infrastructure.Http.Core
{
    public class ApiHttpErrorResponse : IHttpErrorResponse
    {
        public string Title { get; init; }
        public int Status { get; init; }
        public List<ErrorDetail>? Errors { get; init; }

        public ApiHttpErrorResponse(string title, int status, List<ErrorDetail>? errors)
        {
            Title = title;
            Status = status;
            Errors = errors;
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