using Microsoft.EntityFrameworkCore;
using UserService.Domain.Contracts;
using UserService.Domain.Interfaces;
using UserService.Host.Models;
using UserService.Infrastructure.Context;

namespace UserService.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;
    
    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task AddUserAsync(UserAddRequest request)
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

        var data = await _context.Users
            .Where(x => x.UserId == id)
            .Select
            (x => new 
                { x.UserId , x.Username, x.Password, x.Email, x.EmailConfirmed, x.PhoneNumber })
            .AsNoTracking()
            .FirstOrDefaultAsync();  
        
        if (data is null)
        {
            return null;
        }

        return new UserGetResponse(data.UserId, data.Username, data.Password, data.Email,data.EmailConfirmed, data.PhoneNumber);
    }

    public async Task<List<UserGetResponse>> GetUsersAsync()
    {
        var data = await _context.Users
            .Select(x=> new UserGetResponse(x.UserId, x.Username, x.Password, x.Email, x.EmailConfirmed,x.PhoneNumber))
            .AsNoTracking()
            .ToListAsync();
        
        return data;
    }

    public async Task<UserResponse?> UpdateUserByIdAsync(UserUpdateRequest request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == request.Id);

        if (user is null)
        {
            return null;
        }
        
        user.Username = request.Username;
        user.Password = request.Password;
        user.Email = request.Email;
        user.PhoneNumber = request.PhoneNumber;

        _context.Users.Update(user);
        await _context.SaveChangesAsync();

        return new UserResponse(user.UserId, user.Username, user.Email, user.EmailConfirmed, user.PhoneNumber);
    }

    public async Task<bool> DeleteUserByIdAsync(Guid id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);
        
        if (user is null)
        {
            return false;
        }
        
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        
        return true;
    }
    
    public async Task<bool> IsEmailUniqueAsync(string email)
    {
        return !await _context.Users.AnyAsync(x => x.Email == email);
    }
    
    public async Task<bool> IsPhoneNumberUniqueAsync(string phoneNumber)
    {
        return !await _context.Users.AnyAsync(x=> x.PhoneNumber == phoneNumber);
    }

    public async Task<bool> IsUsernameUniqueAsync(string userName)
    {
        return !await _context.Users.AnyAsync(x => x.Username == userName);
    }

    public async Task<bool> IsUserExist(Guid userId)
    {
        return await _context.Users.AnyAsync(x => x.UserId == userId);
    }
}