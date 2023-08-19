using UserService.Config.Database;
using Microsoft.Extensions.Options;
using Serilog;
using UserService.Shared.Infrastructure.Http;
using UserService.Shared.Infrastructure.Http.Routes;
using UserService.Shared.Infrastructure.Http.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using UserService.Config.Logs;
using UserService.Modules.User.Domain.Repositories;
using UserService.Modules.User.Infrastructure.Persistence.Repositories;
using UserService.Modules.User.Application.CreateUser;
using FluentValidation;
using UserService.Modules.User.Application.Abstractions.Behaviors;
using UserService.Modules.User.Application.GetUserByEmail;

var builder = WebApplication.CreateBuilder(args);

//setup MediatR
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

//TODO: improve this
builder.Services.AddScoped<IUserReadRepository, UserReadRepository>();
builder.Services.AddScoped<IUserWriteRepository, UserWriteRepository>();

builder.Services.AddScoped<IValidator<CreateUserCommand>, CreateUserValidator>();
builder.Services.AddScoped<IValidator<GetUserByEmailQuery>, GetUserByEmailValidator>();

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