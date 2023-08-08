using UserService.Config.Database;
using Microsoft.Extensions.Options;
using Serilog;
using UserService.Shared.Infrastructure.Http;
using UserService.Shared.Infrastructure.Http.Routes;
using UserService.Shared.Infrastructure.Http.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

//configure logger
const string appJsonFIle = "appsettings.json";
string appJsonFileEnv = $"appsettings.{env}.json";

var configuration = new ConfigurationBuilder()
     .SetBasePath(Directory.GetCurrentDirectory())
     .AddJsonFile(appJsonFIle, false, true)
     .AddJsonFile(appJsonFileEnv, true)
     .Build();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

builder.Host.UseSerilog(Log.Logger);

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