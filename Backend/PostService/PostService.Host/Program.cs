using PostService.Domain.Constants;
using PostService.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

var connectionStringDb = builder.Environment.IsDevelopment()
    ? builder.Configuration.GetConnectionString(ConnectionStrings.ConnectionStringDb)!
    : Environment.GetEnvironmentVariable(ConnectionStrings.ConnectionStringDb)!;

builder.Services.AddBusinessLogic(connectionStringDb);

var app = builder.Build();


app.Run();