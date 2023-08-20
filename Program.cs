using Serilog;
using UserService.Shared.Infrastructure.Http;
using UserService.Shared.Infrastructure.Http.Routes;
using UserService.Config.Logs;
using UserService.Modules.User.Application.Abstractions.Behaviors;
using UserService.Config;

var builder = WebApplication.CreateBuilder(args);

//setup MediatR
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

//configure logger
builder.Host.UseSerilog(SerilogConfig.Configure());

// configure dependency injection
builder.Services.InstallServices(builder.Configuration, typeof(Program).Assembly);

// configure api versioning
builder.ConfigureVersioning();

// configure http server and run
Server server = new(builder);
server.Run();