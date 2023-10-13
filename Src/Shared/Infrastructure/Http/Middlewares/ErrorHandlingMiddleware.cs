namespace UserService.Shared.Infrastructure.Http.Middlewares
{
    using System.Linq;
    using System.Net;
    using FluentValidation;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Http;
    using Serilog;
    using UserService.Shared.Infrastructure.Http.Core;

    public static class ErrorHandlingMiddlewareExtensions
    {
        public static void UseErrorHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(new ExceptionHandlerOptions
            {
                ExceptionHandler = async context =>
                {
                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                    var exception = exceptionHandlerPathFeature?.Error;

                    if (exception is ValidationException validationException)
                    {

                        var errorDetails = validationException.Errors
                            .Select(x => new ErrorDetail(x.PropertyName, x.ErrorMessage))
                            .ToList();

                        ApiHttpErrorResponse errResponse = new("Bad Request", (int)HttpStatusCode.BadRequest, errorDetails);

                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                        await context.Response.WriteAsJsonAsync(errResponse);
                    }
                    else
                    {
                        ApiHttpResponse response = new("Internal Server Error", (int)HttpStatusCode.InternalServerError);

                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                        Log.Error(exception, "An error occurred while processing the request");

                        await context.Response.WriteAsJsonAsync(response);
                    }
                }
            });
        }
    }
}