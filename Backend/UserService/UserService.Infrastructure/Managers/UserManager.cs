using Microsoft.EntityFrameworkCore;
using UserService.Domain.DTO;
using UserService.Domain.Interfaces;
using UserService.Host.Models;
using UserService.Infrastructure.Context;

namespace UserService.Infrastructure.Managers;

public class UserManager : IUserManager
{
    protected readonly ApplicationDbContext _context;
    
    public UserManager(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async void AddUserAsync(UserAddRequest request)
    {
        var user = new User
        {
            Username = request.Email,
            Password = request.Password,
            Email = request.Email,
            EmailConfirmed = false,
            PhoneNumber = request.PhoneNumber,
        };
        
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task<UserGetResponse> GetUserByIdAsync(Guid id)
    {
        var data = await _context.Users.Select(x=> new UserGetResponse
            (x.UserId, x.Username, x.Password, x.Email, x.EmailConfirmed,x.PhoneNumber)).FirstOrDefaultAsync(x => x.UserId == id);
        if (data is null)
        {
            return null;
        }

        return data;
    }

    public async Task<List<UserGetResponse>> GetUsersAsync()
    {
        var data = await _context.Users.Select(x=> new UserGetResponse
            (x.UserId, x.Username, x.Password, x.Email, x.EmailConfirmed,x.PhoneNumber)).ToListAsync();
        
        return data;
    }
}