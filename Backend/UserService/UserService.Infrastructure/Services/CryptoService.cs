namespace UserService.Infrastructure.Services;

public static class CryptoService
{
    public static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public static bool Verify(string hashPassword, string password)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashPassword);
    }
}