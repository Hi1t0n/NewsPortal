namespace UserService.Domain.Interfaces;

public interface ICryptoService
{
    /// <summary>
    /// Хэширование пароля
    /// </summary>
    /// <param name="password">Пароль</param>
    /// <returns>Хэш пароля</returns>
    string HashPassword(string password);
    /// <summary>
    /// Верификация пароля
    /// </summary>
    /// <param name="hashPassword">Хэш пароля</param>
    /// <param name="password">Пароль</param>
    /// <returns>Результат сравнения паролей</returns>
    bool VerifyPassword(string hashPassword, string password);
}