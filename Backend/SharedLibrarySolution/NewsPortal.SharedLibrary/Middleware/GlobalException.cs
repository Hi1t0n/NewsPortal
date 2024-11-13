using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.Logs;

namespace SharedLibrary.Middleware;

/// <summary>
/// Middleware для глобальной обработки исключений
/// </summary>
/// <param name="next">Следующий делегат запроса <see cref="RequestDelegate"/></param>
public class GlobalException(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    /// <summary>
    ///  Глобальная обработка исключений
    /// </summary>
    /// <param name="context">Контекст HTTP-запроса <see cref="HttpContext"/></param>
    public async Task InvokeAsync(HttpContext context)
    {
        string message = "Ошибка внутреннего сервера. Попробуйте еще раз";
        int statusCode = (int)HttpStatusCode.InternalServerError;
        string title = "Ошибка";

        try
        {
            await _next(context);

            // Проверка ответа на большое число запросов // 429 статус код
            if (context.Response.StatusCode == StatusCodes.Status429TooManyRequests)
            {
                title = "Предупреждение";
                message = "Сделано слишком много запросов.";
                statusCode = StatusCodes.Status429TooManyRequests;
                await ModifyHeader(context, title, message, statusCode);
            }

            // Проверка ответа на отсутствие прав доступа // 401 статус код
            if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
            {
                title = "Тревога";
                message = "У вас нет прав доступа.";
                statusCode = StatusCodes.Status401Unauthorized;
                await ModifyHeader(context, title, message, statusCode);
            }

            // Проверка ответа на доступ запрещен // 403 статус код
            if (context.Response.StatusCode == StatusCodes.Status403Forbidden)
            {
                title = "У вас нет доступа";
                message = "Вам запрещено переходить сюда";
                statusCode = StatusCodes.Status403Forbidden;
                await ModifyHeader(context, title, message, statusCode);
            }
        }
        catch (Exception exception) when (exception is TaskCanceledException || exception is TimeoutException)
        {
            LogException.LogExceptions(exception);
            title = "Время ожидания ответа истекло";
            message = "Время ожидания ответа истекло. Попробуйте еще раз";
            statusCode = StatusCodes.Status408RequestTimeout;
            await ModifyHeader(context, title, message, statusCode);
        }
        catch (Exception exception)
        {
            // Логируем исходные исключения
            LogException.LogExceptions(exception);
            await ModifyHeader(context, title, message, statusCode);
        }
    }

    /// <summary>
    /// Изменение заголовков ответа
    /// </summary>
    /// <param name="context">Контекст HTTP-запроса <see cref="HttpContext"/></param>
    /// <param name="title">Заголовок ошибки</param>
    /// <param name="message">Сообщение об ошибки</param>
    /// <param name="statusCode">Код состояния HTTP-ответа</param>
    private static async Task ModifyHeader(HttpContext context, string title, string message, int statusCode)
    {
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(JsonSerializer.Serialize(new ProblemDetails
        {
            Detail = message,
            Status = statusCode,
            Title = title,
        }), CancellationToken.None);
    }
}