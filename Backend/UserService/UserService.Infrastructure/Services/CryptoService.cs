using UserService.Domain.Interfaces;

namespace UserService.Infrastructure.Services;

/// <summary>
///  Реализация интерфейса <see cref="ICryptoService"/>
/// </summary>
public class CryptoService : ICryptoService
{
    /// <inheritdoc/>
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.EnhancedHashPassword(password);
    }
    
    /// <inheritdoc/>
    public bool VerifyPassword(string hashPassword, string password)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(password, hashPassword);
    }
}