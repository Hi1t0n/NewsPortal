namespace UserService.Domain.Models;

public class Role
{
    public required Guid Id { get; set; } = Guid.Empty;
    public string? Name { get; set; } = string.Empty;

    public List<User>? Users { get; set; } = new List<User>();
}