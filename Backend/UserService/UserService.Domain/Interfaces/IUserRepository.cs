using UserService.Domain.Models;

namespace UserService.Domain.Interfaces;

/// <summary>
/// Интерфейс репозитория <see cref="User"/>
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Добавление пользователя
    /// </summary>
    /// <param name="user">Пользователь <see cref="User"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Добавленный пользователь</returns>
    Task<User?> AddUserAsync(User user, CancellationToken cancellationToken);
    /// <summary>
    /// Получение пользователя по id
    /// </summary>
    /// <param name="id">Идентификатор пользователя</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Данные пользователя</returns>
    Task<User?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken);
    /// <summary>
    /// Получение всех пользователей
    /// </summary>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Коллекция всех пользователей</returns>
    Task<IReadOnlyCollection<User>?> GetUsersAsync(CancellationToken cancellationToken);
    /// <summary>
    /// Обновление пользователя по id
    /// </summary>
    /// <param name="id">Идентификатор пользователя</param>
    /// <param name="user">Данные пользователя</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Обновленный пользователь</returns>
    Task<User?> UpdateUserByIdAsync(Guid id, User user, CancellationToken cancellationToken);
    /// <summary>
    /// Удаление пользователя по id
    /// </summary>
    /// <param name="id">Идентификатор пользователя</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Удаленный пользователь</returns>
    Task<User?> DeleteUserByIdAsync(Guid id, CancellationToken cancellationToken);
    /// <summary>
    /// Восстановление пользователя по id
    /// </summary>
    /// <param name="id">Идентификатор пользователя</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Восстановленный пользователь</returns>
    Task<User?> RestoreUserByIdAsync(Guid id, CancellationToken cancellationToken);
    /// <summary>
    /// Проверка существует ли email в БД
    /// </summary>
    /// <param name="email">Электронная почта</param>
    /// <returns>True, если email существует, иначе false</returns>
    Task<bool> ExistByEmail(string? email);
    /// <summary>
    /// Проверка существует ли номер телефона в БД
    /// </summary>
    /// <param name="phoneNumber">Электронная почта</param>
    /// <returns>True, если номер существует, иначе false</returns>
    Task<bool> ExistByPhoneNumber(string? phoneNumber);
    /// <summary>
    /// Проверка существует ли имя пользователя в БД
    /// </summary>
    /// <param name="userName">Электронная почта</param>
    /// <returns>True, если userName существует, иначе false</returns>
    Task<bool> ExistByUserName(string userName);
}