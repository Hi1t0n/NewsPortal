using UserService.Domain.Contacts;

namespace UserService.Domain.Models;

public class User
{
    public Guid UserId { get; set; } = Guid.Empty;
    public string? UserName { get; set; } = string.Empty;
    public string? Password { get; set; } = string.Empty;
    public Guid? RoleId { get; set; }
    public string? Email { get; set; } = string.Empty;
    public bool EmailConfirmed { get; set; } = false;
    public string? PhoneNumber { get; set; } = string.Empty;
    public bool PhoneNumberConfirmed { get; set; } = false;
    public bool IsDelete { get; set; } = false;
    public Role? Role { get; set; }

    public UserResponseContract ToResponse()
    {
        return new UserResponseContract(UserId, UserName!, Role!.RoleName!, Email, EmailConfirmed,
            PhoneNumber, PhoneNumberConfirmed);
    }
}