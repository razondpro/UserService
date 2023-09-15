using Serilog;
using UserService.Shared.Infrastructure.Http;
using UserService.Shared.Infrastructure.Http.Routes;
using UserService.Config;
using UserService.Config.Logs;

var builder = WebApplication.CreateBuilder(args);

//configure logger
builder.Host.UseSerilog(SerilogConfig.Configure());

// configure dependency injection
builder.Services.InstallServices(builder.Configuration, typeof(Program).Assembly);

// configure api versioning
builder.ConfigureVersioning();

// configure http server and run
Server server = new(builder);
await server.RunAsync();