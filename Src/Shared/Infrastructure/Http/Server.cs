namespace UserService.Shared.Infrastructure.Http
{
    using Serilog;
    using Shared.Infrastructure.Http.Api;

    public class Server
    {

        private readonly WebApplication App;
        public Server(WebApplicationBuilder builder)
        {
            App = builder.Build();

            ConfigureMiddlewares();

            ConfigureRoutes();

            ConfigureSwagger();
        }

        private void ConfigureMiddlewares()
        {
            //logger http middleware
            App.UseSerilogRequestLogging();
            //if is dev env, use developer exception page
            if (App.Environment.IsDevelopment())
            {
                App.UseDeveloperExceptionPage();
            }

            App.UseExceptionHandler(exceptionHandlerApp
                     => exceptionHandlerApp.Run(async context
                     => await Results.Problem()
                        .ExecuteAsync(context)));
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