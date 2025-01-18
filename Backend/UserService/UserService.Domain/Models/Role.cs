namespace UserService.Domain.Models;

public class Role
{
    public Guid RoleId { get; set; } = Guid.Empty;
    public string? RoleName { get; set; } = string.Empty;
    public ICollection<User> Users { get; set; } = new List<User>();
}