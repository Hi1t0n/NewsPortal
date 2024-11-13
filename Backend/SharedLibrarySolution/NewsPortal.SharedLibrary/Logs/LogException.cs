using Serilog;

namespace SharedLibrary.Logs;

public static class LogException
{
    /// <summary>
    /// Логируем исключения
    /// </summary>
    /// <param name="exception">Любое исключение <see cref="Exception"/></param>
    public static void LogExceptions(Exception exception)
    {
        LogToFile(exception.Message);
        LogToConsole(exception.Message);
        LogToDebug(exception.Message);
    }
    
    /// <summary>
    /// Лог в дебагер
    /// </summary>
    /// <param name="exceptionMessage">Текст исключения</param>
    public static void LogToDebug(string exceptionMessage) => Log.Debug(exceptionMessage);
    
    /// <summary>
    /// Лог в консоль
    /// </summary>
    /// <param name="exceptionMessage">Текст исключения</param>
    public static void LogToConsole(string exceptionMessage) => Log.Warning(exceptionMessage);
    
    /// <summary>
    /// Лог в файл
    /// </summary>
    /// <param name="exceptionMessage">Текст исключения</param>
    public static void LogToFile(string exceptionMessage) => Log.Information(exceptionMessage);
}