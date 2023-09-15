
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.Modules.User.Application.CreateUser;
using UserService.Modules.User.Application.GetUserByEmail;

namespace UserService.Modules.User.Infrastructure.Http.Routes
{
    public static class UserRouteExtensions
    {

        public static RouteGroupBuilder MapUserRoutes(this RouteGroupBuilder builder)
        {

            builder.MapPost("/", async (IMediator mediator, CreateUserCommand req) =>
            {
                return await mediator.Send(req);
            })
            .WithName("CreateUser")
            .WithDescription("Create a new user");



            builder.MapGet("/{email}", async (IMediator mediator, string email) =>
            {
                return await mediator.Send(new GetUserByEmailQuery(email));
            })
            .WithName("GetUserByEmail")
            .WithDescription("Get user by email");

            return builder;
        }
    }

}
