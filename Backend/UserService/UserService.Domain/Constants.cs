namespace UserService.Domain;

public static class Constants
{
    public static readonly int RetryOnFailure = 10;
    /// <summary>
    /// Строка подключения к бд для Development
    /// </summary>
    public static readonly string ConnectionStringConfiguration = "PostgreSQL";
    /// <summary>
    /// Строка подключения к бд для Production
    /// </summary>
    public static readonly string CoonectionStringEnvironment = "CONNECTION_STRING_USER_SERVICE";
    /// <summary>
    /// Строка подключения к Redis
    /// </summary>
    public static readonly string ConnectionStringRedis = "CONNECTION_STRING_REDIS_USER_SERVICE";
}