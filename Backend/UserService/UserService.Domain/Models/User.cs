using UserService.Domain.Models;

namespace UserService.Host.Models;

public class User
{
    public Guid UserId { get; set; } = Guid.NewGuid();
    public required string Username { get; set; } = string.Empty;
    public required string Password { get; set; } = string.Empty;
    public required string Email { get; set; } = string.Empty;
    public required bool EmailConfirmed { get; set; }
    public required string PhoneNumber { get; set; } = string.Empty;
}