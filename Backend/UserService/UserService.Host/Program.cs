using UserService.Domain;
using UserService.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Environment.IsDevelopment()
    ? builder.Configuration.GetConnectionString(Constants.ConnectionStringConfiguration)!
    : Environment.GetEnvironmentVariable(Constants.CoonectionStringEnvironment)!;

builder.Services.AddBusinessLogic(builder.Configuration, connectionString);

var app = builder.Build();

app.Run();