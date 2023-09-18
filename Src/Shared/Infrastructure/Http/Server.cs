namespace UserService.Shared.Infrastructure.Http
{
    using Serilog;
    using Shared.Infrastructure.Http.Api;
    using UserService.Shared.Infrastructure.Http.Middlewares;

    public class Server
    {
        private readonly WebApplication _app;
        public Server(WebApplicationBuilder builder)
        {
            _app = builder.Build();

            ConfigureAuthentication();

            ConfigureMiddlewares();

            ConfigureRoutes();

            ConfigureSwagger();

            configureGlobalErrorHandling();

            ConfigureAppLifetimeEvents();
        }

        private void ConfigureAppLifetimeEvents()
        {
            _app.Services.GetRequiredService<IHostApplicationLifetime>()
                .ApplicationStopping.Register(OnApplicationStopping);
        }
        public void OnApplicationStopping()
        {
            Log.Information("Stopping application");
            //TODO: add logic to stop application
        }

        private void ConfigureAuthentication()
        {
            _app.UseAuthentication();
            _app.UseAuthorization();
        }

        private void ConfigureMiddlewares()
        {
            //json validation middleware
            _app.UseMiddleware<JsonValidationMiddleware>();
            //logger http middleware
            _app.UseSerilogRequestLogging();
            //if is dev env, use developer exception page
            if (_app.Environment.IsDevelopment())
            {
                _app.UseDeveloperExceptionPage();
            }
        }

        private void configureGlobalErrorHandling()
        {
            _app.UseErrorHandlingMiddleware();
        }

        private void ConfigureSwagger()
        {
            if (_app.Environment.IsDevelopment())
            {
                _app.UseSwagger();
                _app.UseSwaggerUI(
                    options =>
                    {
                        var descriptions = _app.DescribeApiVersions();
                        // build a swagger endpoint for each discovered API version
                        foreach (var description in descriptions)
                        {
                            var url = $"/swagger/{description.GroupName}/swagger.json";
                            var name = description.GroupName.ToUpperInvariant();
                            options.SwaggerEndpoint(url, name);
                        }
                    });
            }
        }

        private void ConfigureRoutes()
        {
            _app.NewVersionedApi()
                .BuildRoutes();
        }

        public async Task RunAsync()
        {
            Log.Information("Starting web host");
            await _app.RunAsync();
        }
        public async Task StopAsync()
        {
            Log.Information("Stopping web host");
            await _app.StopAsync();
        }
    }
}