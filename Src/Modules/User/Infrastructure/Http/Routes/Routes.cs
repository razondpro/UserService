
using MediatR;
using UserService.Modules.User.Application.CreateUser;

namespace UserService.Modules.User.Infrastructure.Http.Routes
{
    public static class UserRouteExtensions
    {

        public static RouteGroupBuilder MapUserRoutes(this RouteGroupBuilder builder)
        {

            builder.MapPost("/", async (IMediator mediator, CreateUserCommand dto) =>
            {
                var cmd = new CreateUserCommand("Razon", "miah", "miahrazon@gmail.com", "nozar");
                return await mediator.Send(cmd);
            })
            .WithName("CreateUser")
            .WithDescription("Create a new user");

            // implement the rest of the routes here

            return builder;
        }
    }

}
