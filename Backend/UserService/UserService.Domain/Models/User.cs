namespace UserService.Domain.Models;

public class User
{
    public Guid UserId { get; set; }
    public required string UserName { get; set; }
    public required string Password { get; set; }
    public string? Email { get; set; } = string.Empty;
    public bool EmailConfirmed { get; set; } = false;
    public string? PhoneNumber { get; set; } = string.Empty;
    public bool PhoneNumberConfirmed { get; set; } = false;
}