namespace UserService.Shared.Infrastructure.Http
{
    using System.Text.Json;
    using Serilog;
    using Shared.Infrastructure.Http.Api;
    using UserService.Shared.Infrastructure.Http.Middlewares;

    public class Server
    {

        private readonly WebApplication App;
        public Server(WebApplicationBuilder builder)
        {
            App = builder.Build();

            ConfigureMiddlewares();

            ConfigureRoutes();

            configureGlobalErrorHandling();

            ConfigureSwagger();
        }

        private void ConfigureMiddlewares()
        {
            //json validation middleware
            App.UseMiddleware<JsonValidationMiddleware>();
            //logger http middleware
            App.UseSerilogRequestLogging();
            //if is dev env, use developer exception page
            if (App.Environment.IsDevelopment())
            {
                App.UseDeveloperExceptionPage();
            }
        }

        private void configureGlobalErrorHandling()
        {
            App.UseErrorHandlingMiddleware();
        }

        private void ConfigureSwagger()
        {
            App.UseSwagger();
            App.UseSwaggerUI(
                options =>
                {
                    var descriptions = App.DescribeApiVersions();

                    // build a swagger endpoint for each discovered API version
                    foreach (var description in descriptions)
                    {
                        var url = $"/swagger/{description.GroupName}/swagger.json";
                        var name = description.GroupName.ToUpperInvariant();
                        options.SwaggerEndpoint(url, name);
                    }
                });
        }

        private void ConfigureRoutes()
        {
            var app = App.NewVersionedApi();
            ApiBuilder.BuildRoutes(app);
        }

        public void Run()
        {
            Log.Information("Starting web host");
            App.Run();
        }
    }
}