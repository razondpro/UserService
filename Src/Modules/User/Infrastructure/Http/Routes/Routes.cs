
namespace Modules.User.Infrastructure.Http.Routes
{

    public static class UserRouteExtensions
    {

        public static RouteGroupBuilder MapUserRoutes(this RouteGroupBuilder builder)
        {

            builder.MapPut("/", (HttpContext context) =>
            {
                return Results.NoContent();
            });

            // implement the rest of the routes here

            return builder;
        }
    }

}
