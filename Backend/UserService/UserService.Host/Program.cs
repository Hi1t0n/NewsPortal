using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.DependencyInjection;
using UserService.Domain.Validators;
using UserService.Host.Extensions;
using UserService.Host.Routes;
using UserService.Infrastructure.Context;
using UserService.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Environment.IsDevelopment()
    ? builder.Configuration.GetConnectionString("PostgreSQL")
    : Environment.GetEnvironmentVariable("CONNECTION_STRING_USER_SERVICE");
var connectionStringRedis = Environment.GetEnvironmentVariable("CONNECTION_STRING_REDIS");
var fileName = builder.Configuration.GetSection("Logger:FileName").Value;

builder.Services.AddBusinessLogic(builder.Configuration,connectionString!, connectionStringRedis!, fileName!);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddValidatorsFromAssemblyContaining(typeof(UserAddRequestValidator));
builder.Services.AddValidatorsFromAssemblyContaining(typeof(UserUpdateRequestValidator));

builder.Services.AddCors();

var app = builder.Build();

app.UseSharedMiddleware();

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