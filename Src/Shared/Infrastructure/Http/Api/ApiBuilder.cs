namespace UserService.Shared.Infrastructure.Http.Api
{
    using Asp.Versioning.Builder;
    using Modules.User.Infrastructure.Http.Routes;
    using Shared.Infrastructure.Http.Routes;

    public static class ApiBuilder
    {
        public static IVersionedEndpointRouteBuilder BuildRoutes(this IVersionedEndpointRouteBuilder application)
        {
            var apiV1 = application.MapGroup("/api/").HasApiVersion(VersioningExtensions.V1);

            //health check
            apiV1.MapHealthChecks("/healthz");
            //users routes v1
            var usersApiV1 = apiV1.MapGroup("/users");
            UserRouteExtensions.MapUserRoutes(usersApiV1).WithTags("Users");

            return application;
        }
    }
}

