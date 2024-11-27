using UserService.Host.Models;

namespace UserService.Domain.Models;

public class Role
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Name { get; set; } = string.Empty;
}