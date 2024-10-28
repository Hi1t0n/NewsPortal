using UserService.Domain.Contracts;
using UserService.Host.Models;

namespace UserService.Domain.Interfaces;

/// <summary>
/// Интерфейс репозитория пользователей
/// </summary>
public interface IUserRepository
{ 
    /// <summary>
    /// Метод добавления пользователя
    /// </summary>
    /// <param name="request">DTO с данными пользователя</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns>Созданный пользователь</returns>
    Task<User> AddUserAsync(UserAddRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Метод получения данных пользователя по Id
    /// </summary>
    /// <param name="id">Идентиффикатор пользователя</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns>Данные пользователя</returns>
    Task<UserResponse?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken);
    
    /// <summary>
    /// Метод получения всех пользователей
    /// </summary>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns>Список всех пользователей</returns>
    Task<List<UserResponse>> GetUsersAsync(CancellationToken cancellationToken);
    
    /// <summary>
    /// Метод обновления данных пользователя
    /// </summary>
    /// <param name="request">DTO с новыми данными</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns>Новые данные пользователя или null</returns>
    Task<UserResponse?> UpdateUserByIdAsync(UserUpdateRequest request, CancellationToken cancellationToken);
    
    /// <summary>
    /// Метод удаления пользователя по Id
    /// </summary>
    /// <param name="id">Идентификатор пользователя</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns>Результат удаления</returns>
    Task<bool> DeleteUserByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> IsEmailUniqueAsync(string email);
    Task<bool> IsPhoneNumberUniqueAsync(string phoneNumber);
    Task<bool> IsUsernameUniqueAsync(string userName);
    Task<bool> IsUserExist(Guid userId);
}