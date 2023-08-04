using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


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




var app = builder.Build();

bool isDev = app.Environment.IsDevelopment();

//logger http middleware
app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (isDev)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

Log.Information("Starting web host");

app.UseAuthorization();

app.MapControllers();

app.Run();
