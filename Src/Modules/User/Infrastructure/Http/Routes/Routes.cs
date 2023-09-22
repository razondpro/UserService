namespace UserService.Modules.User.Infrastructure.Http.Routes
{
    using UserService.Modules.User.Application.CreateUser;
    using UserService.Modules.User.Application.FindUserByEmail;
    public static class UserRouteExtensions
    {

        public static RouteGroupBuilder MapUserRoutes(this RouteGroupBuilder builder)
        {

            builder.MapPost("/", async (CreateUserHttpController controller, CreateUserRequestDto req) =>
            {
                return await controller.execute(req);
            })
            .WithName("CreateUser")
            .WithDescription("Create a new user");


            builder.MapGet("/{email}", async (FindUserByEmailHttpController controller, string email) =>
            {
                return await controller.execute(new FindUserByEmailRequestDto(email));
            })
            .WithName("GetUserByEmail")
            .WithDescription("Get user by email");

            return builder;
        }
    }

}
