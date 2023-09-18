namespace UserService.Modules.User.Infrastructure.Http.Routes
{
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.IdentityModel.Tokens;
    using UserService.Modules.User.Application.CreateUser;
    using UserService.Modules.User.Application.GetUserByEmail;
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
            .RequireAuthorization()
            .WithName("GetUserByEmail")
            .WithDescription("Get user by email");

            return builder;
        }
    }

}
