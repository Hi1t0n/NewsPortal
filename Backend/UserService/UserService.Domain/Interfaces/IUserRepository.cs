using UserService.Domain.Models;

namespace UserService.Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> AddUserAsync(User user, CancellationToken cancellationToken);
    Task<User?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<User>> GetUsersAsync(CancellationToken cancellationToken);
    Task<User?> UpdateUserByIdAsync(Guid id, User user, CancellationToken cancellationToken);
    Task<User?> DeleteUserByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<User?> RestoreUserByIdAsync(Guid id, CancellationToken cancellationToken);
}