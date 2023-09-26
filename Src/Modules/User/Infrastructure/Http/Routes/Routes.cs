namespace UserService.Modules.User.Infrastructure.Http.Routes
{
    using Microsoft.AspNetCore.Mvc;
    using UserService.Modules.User.Application.CreateUser;
    using UserService.Modules.User.Application.FindUserByEmail;
    using UserService.Modules.User.Application.UpdateUser;
    using UserService.Modules.User.Infrastructure.Http.Filters;
    using UserService.Shared.Infrastructure.Http.Core;

    public static class UserRouteExtensions
    {
        public static RouteGroupBuilder MapUserRoutes(this RouteGroupBuilder builder)
        {

            builder.MapPost("/", async (
                CancellationToken cancellationToken,
                CreateUserHttpController controller,
                CreateUserRequestDto req) =>
            {
                return await controller.Execute(req, cancellationToken);
            })
            .AddEndpointFilter<ValidationFilter<CreateUserRequestDto>>()
            .WithName("CreateUser")
            .WithDescription("Create a new user")
            .Produces<ApiHttpResponse>(StatusCodes.Status400BadRequest);

            builder.MapPut("/", async (
                CancellationToken cancellationToken,
                UpdateUserHttpController controller,
                [FromBody] UpdateUserRequestDto req) =>
            {
                return await controller.Execute(req, cancellationToken);
            })
            .AddEndpointFilter<ValidationFilter<UpdateUserRequestDto>>()
            .WithName("UpdateUser")
            .WithDescription("Update an user")
            .Produces<ApiHttpResponse>(StatusCodes.Status400BadRequest);


            builder.MapGet("/{email}", async (
                HttpContext context,
                CancellationToken cancellationToken,
                FindUserByEmailHttpController controller,
                [AsParameters] FindUserByEmailRequestDto email) =>
            {
                return await controller.Execute(email, cancellationToken);
            })
            .AddEndpointFilter<ValidationFilter<FindUserByEmailRequestDto>>()
            .WithName("GetUserByEmail")
            .WithDescription("Get user by email")
            .Produces<ApiHttpResponse>(StatusCodes.Status400BadRequest);

            return builder;
        }
    }
}
