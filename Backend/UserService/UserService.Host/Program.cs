using UserService.Domain;
using UserService.Host.Endpoints;
using UserService.Host.Extensions;
using UserService.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Environment.IsDevelopment()
    ? builder.Configuration.GetConnectionString(Constants.ConnectionStringConfiguration)!
    : Environment.GetEnvironmentVariable(Constants.CoonectionStringEnvironment)!;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddBusinessLogic(builder.Configuration, connectionString);

var app = builder.Build();

if (true)
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.AddUserEndpoints();

app.ApplyMigrations();

app.Run();