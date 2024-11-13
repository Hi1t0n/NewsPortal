using Microsoft.AspNetCore.Http;
using SharedLibrary.Classes;

namespace SharedLibrary.Middleware;

/// <summary>
/// Middleware для проверки api шлюха
/// </summary>
/// <param name="next">Следующий делегат запроса <see cref="RequestDelegate"/></param>
public class ListenToOnlyApiGateway(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;
    
    public async Task InvokeAsync(HttpContext context)
    {
        var signedHeader = context.Request.Headers[Constants.ApiGateway];
        
        // Если заголовок не найден, значит запрос не исходит из шлюза // 503 статус код
        if (signedHeader.FirstOrDefault() is null)
        {
            context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
            await context.Response.WriteAsync("Служба недоступна");
        }
        else
        {
            await _next(context);
        }
    }
}