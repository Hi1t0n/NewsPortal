namespace UserService.Host.Models;

public class User
{
    public Guid UserId { get; set; } = Guid.NewGuid();
    public required string Username { get; set; } = string.Empty;
    public required string Password { get; set; } = string.Empty;
}