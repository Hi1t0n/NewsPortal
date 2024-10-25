using FluentValidation;
using Microsoft.EntityFrameworkCore;
using UserService.Domain.Validators;
using UserService.Host.Extensions;
using UserService.Host.Routes;
using UserService.Infrastructure.Context;
using UserService.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Environment.IsDevelopment()
    ? builder.Configuration.GetConnectionString("PostgreSQL")
    : Environment.GetEnvironmentVariable("CONNECTION_STRING_USER_SERVICE");

builder.Services.AddBusinessLogic(connectionString!);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddValidatorsFromAssemblyContaining(typeof(UserAddRequestValidator));
builder.Services.AddValidatorsFromAssemblyContaining(typeof(UserUpdateRequestValidator));

builder.Services.AddCors();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseCors(x =>
{
    x.AllowAnyHeader();
    x.WithOrigins("*");
    x.AllowAnyOrigin();
}); 

app.AddUserRouters();
app.ApplyMigrations();

app.Run();