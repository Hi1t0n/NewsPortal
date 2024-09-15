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
    /// <returns>Созданный пользователь</returns>
    Task<User> AddUserAsync(UserAddRequest request);
    
    /// <summary>
    /// Метод получения данных пользователя по Id
    /// </summary>
    /// <param name="id">Идентиффикатор пользователя</param>
    /// <returns>Данные пользователя</returns>
    Task<UserResponse?> GetUserByIdAsync(Guid id);
    
    /// <summary>
    /// Метод получения всех пользователей
    /// </summary>
    /// <returns>Список всех пользователей</returns>
    Task<List<UserResponse>> GetUsersAsync();
    
    /// <summary>
    /// Метод обновления данных пользователя
    /// </summary>
    /// <param name="request">DTO с новыми данными</param>
    /// <returns>Новые данные пользователя или null</returns>
    Task<UserResponse?> UpdateUserByIdAsync(UserUpdateRequest request);
    
    /// <summary>
    /// Метод удаления пользователя по Id
    /// </summary>
    /// <param name="id">Идентификатор пользователя</param>
    /// <returns>Результат удаления</returns>
    Task<bool> DeleteUserByIdAsync(Guid id);
    Task<bool> IsEmailUniqueAsync(string email);
    Task<bool> IsPhoneNumberUniqueAsync(string phoneNumber);
    Task<bool> IsUsernameUniqueAsync(string userName);
    Task<bool> IsUserExist(Guid userId);
}