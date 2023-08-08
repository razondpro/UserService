using UserService.Config.Database;
using Microsoft.Extensions.Options;
using Serilog;
using UserService.Shared.Infrastructure.Http;
using UserService.Shared.Infrastructure.Http.Routes;
using UserService.Shared.Infrastructure.Http.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using UserService.Config.Logs;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//configure logger
builder.Host.UseSerilog(SerilogConfig.Configure());

//configure database
builder.Services.AddDatabase();

// configure api versioning
VersioningExtensions.ConfigureVersioning(builder);

// configure swagger
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen(options => options.OperationFilter<SwaggerDefaultValues>());


// configure http server and run
Server server = new(builder);
server.Run();