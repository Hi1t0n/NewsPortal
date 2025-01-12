using UserService.Domain.Models;

namespace UserService.Domain.Interfaces;

public interface IUserRepository
{
    Task<User> AddUserAsync(User user);
}