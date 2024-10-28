namespace UserService.Domain.Interfaces;

/// <summary>
/// Интерфейс сервиса кэширования
/// </summary>
public interface ICachedService
{
    /// <summary>
    /// Получение данных из кэша
    /// </summary>
    /// <param name="key">Ключ</param>
    /// <param name="cancellationToken">Cancellationtoken</param>
    /// <typeparam name="T">Class</typeparam>
    /// <returns>Объект типа T</returns>
    Task<T?> GetCacheAsync<T>(string key, CancellationToken cancellationToken) where T : class;
    /// <summary>
    /// Добавление данных в кэш
    /// </summary>
    /// <param name="key">Ключ</param>
    /// <param name="data">Данные</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <typeparam name="T">Class</typeparam>
    /// <returns>Void Task</returns>
    Task AddCacheAsync<T>(string key, T? data, CancellationToken cancellationToken) where T : class;
}