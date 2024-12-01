namespace UserService.Domain.Models;

public class User
{
    public required Guid UserId { get; set; } = Guid.Empty;
    public required string Username { get; set; } = string.Empty;
    public required string Password { get; set; } = string.Empty;
    
    public Guid RoleId { get; set; }
    
    public Role? Role { get; set; }
    public required string Email { get; set; } = string.Empty;
    public required bool EmailConfirmed { get; set; }
    public required string PhoneNumber { get; set; } = string.Empty;
    
}