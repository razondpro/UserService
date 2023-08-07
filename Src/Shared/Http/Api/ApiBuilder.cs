using Asp.Versioning.Builder;
using Modules.User.Infrastructure.Http.Routes;
using Shared.Http.Routes;

namespace Shared.Http.Api
{
    public static class ApiBuilder
    {

        public static void BuildRoutes(IVersionedEndpointRouteBuilder application)
        {


            var apiV1 = application.MapGroup("/api/").HasApiVersion(VersioningExtensions.V1);

            //users routes v1
            var usersApiV1 = apiV1.MapGroup("/users");
            UserRouteExtensions.MapUserRoutes(usersApiV1).WithTags("Users");

        }

    }
}

