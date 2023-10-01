using UserService.Shared.Infrastructure.Http;
using UserService.Config;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.InstallHosts(builder.Configuration, typeof(Program).Assembly);
builder.Services.InstallServices(builder.Configuration, typeof(Program).Assembly);

Server server = new(builder);
await server.RunAsync();