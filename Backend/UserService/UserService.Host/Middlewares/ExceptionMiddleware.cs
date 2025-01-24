using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using UserService.Domain.Contacts;

namespace UserService.Host.Middlewares;

public class ExceptionMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception) when (exception is TaskCanceledException || exception is TimeoutException)
        {
            await ModifyHeader(context, new ProblemDetails
            {
                Title = "Request Timeout",
                Detail = exception.Message,
                Status = StatusCodes.Status408RequestTimeout
            });
        }
        catch (Exception exception)
        {
            await ModifyHeader(context, new ProblemDetails
            {
                Title = "Servet Internal Error",
                Detail = exception.Message,
                Status = StatusCodes.Status500InternalServerError
            });
        }
    }

    private static async Task ModifyHeader(HttpContext context, ProblemDetails problemDetails)
    {
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(problemDetails, CancellationToken.None);
    }
}

public static class ExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder applicationBuilder)
    {
        return applicationBuilder.UseMiddleware<ExceptionMiddleware>();
    }
}