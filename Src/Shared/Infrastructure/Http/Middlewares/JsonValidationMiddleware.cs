namespace UserService.Shared.Infrastructure.Http.Middlewares
{
    using System.Text.Json;
    using Microsoft.AspNetCore.Mvc;

    public class JsonValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public JsonValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.ContentType?.StartsWith("application/json") == true)
            {
                var requestBodyStream = new MemoryStream();
                try
                {
                    await context.Request.Body.CopyToAsync(requestBodyStream);
                    requestBodyStream.Position = 0;

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var deserializedObject = await JsonSerializer.DeserializeAsync<object>(requestBodyStream, options);
                }
                catch (JsonException)
                {
                    var problem = new ProblemDetails
                    {
                        Title = "Invalid JSON",
                        Status = StatusCodes.Status400BadRequest,
                        Detail = "Request body is not valid JSON"
                    };
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsJsonAsync(problem);
                    return;
                }
                finally
                {
                    // Reset the request body stream position so that it can be read again by other middlewares
                    requestBodyStream.Position = 0;
                    context.Request.Body = requestBodyStream;
                }
            }

            await _next(context);
        }
    }
}