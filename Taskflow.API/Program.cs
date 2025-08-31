using Taskflow.API.Config;
using Taskflow.API.Middleware;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
configuration.AddJsonFile($"appsettings.{env}.json", true, true);

builder.Services.AddConfig(configuration);
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(opt => { opt.DefaultModelsExpandDepth(-1); });

// Middleware de manejo de errores
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.UseCors();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();