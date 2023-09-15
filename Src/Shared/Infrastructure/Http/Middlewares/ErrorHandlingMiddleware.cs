namespace UserService.Shared.Infrastructure.Http.Middlewares
{
    using System.Linq;
    using System.Net;
    using FluentValidation;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Serilog;

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
                        var errors = validationException.Errors
                            .Select(x => new { x.PropertyName, x.ErrorMessage })
                            .ToList();

                        var response = new ProblemDetails
                        {
                            Title = "Bad Request",
                            Status = (int)HttpStatusCode.BadRequest,
                        };
                        response.Extensions.Add("errors", errors);

                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                        await context.Response.WriteAsJsonAsync(response);
                    }
                    else
                    {
                        var response = new ProblemDetails
                        {
                            Title = "Internal Server Error",
                            Status = (int)HttpStatusCode.InternalServerError,
                        };

                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                        Log.Error(exception, "An error occurred while processing the request");

                        await context.Response.WriteAsJsonAsync(response);
                    }
                }
            });
        }
    }
}