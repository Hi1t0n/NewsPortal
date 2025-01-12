using UserService.Domain.Interfaces;
using UserService.Domain.Models;
using UserService.Infrastructure.Context;

namespace UserService.Infrastructure.Repository;

public class UserRepository(ApplicationDbContext context) : IUserRepository
{
    private readonly ApplicationDbContext _context = context;
    
    public async Task<User> AddUserAsync(User user)
    {
        await _context.AddAsync(user);
        await _context.SaveChangesAsync();

        return user;
    }
}