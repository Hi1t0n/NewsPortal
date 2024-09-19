using NewsService.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddBusinessLogic(builder.Configuration.GetConnectionString("PostgreSQL")!);
builder.Services.AddSwaggerGen();
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

app.Run();