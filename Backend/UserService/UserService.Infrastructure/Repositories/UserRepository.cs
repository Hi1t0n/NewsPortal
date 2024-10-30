using Microsoft.EntityFrameworkCore;
using UserService.Domain.Contracts;
using UserService.Domain.Interfaces;
using UserService.Host.Models;
using UserService.Infrastructure.Context;

namespace UserService.Infrastructure.Repositories;

/// <summary>
/// <see cref="IUserRepository"/>
/// </summary>
public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    private readonly ICryptoService _cryptoService;
    private readonly ICachedService _cachedService;
    
    public UserRepository(ApplicationDbContext context, ICryptoService cryptoService, ICachedService cachedService)
    {
        _context = context;
        _cryptoService = cryptoService;
        _cachedService = cachedService;
    }
    
    /// <inheritdoc/>
    public async Task<User> AddUserAsync(UserAddRequest request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Username = request.Email,
            Password = _cryptoService.HashPassword(request.Password),
            Email = request.Email,
            EmailConfirmed = false,
            PhoneNumber = request.PhoneNumber,
        };
        
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        
        return user;
    }

    /// <inheritdoc/>
    public async Task<UserResponse?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var data = await _cachedService.GetCacheAsync<UserResponse>(id.ToString(), cancellationToken);

        if (data is null)
        {
            data = await _context.Users
                .Where(x => x.UserId == id)
                .Select(x => 
                    new UserResponse(x.UserId, x.Username, x.Email,x.EmailConfirmed,x.PhoneNumber))
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
        
        if (data is null)
        {
            return null;
        }
        
        await _cachedService.AddCacheAsync<UserResponse>(data.Id.ToString(), data, cancellationToken);
        
        return data;
    }

    /// <inheritdoc/>
    public async Task<List<UserResponse>> GetUsersAsync(CancellationToken cancellationToken)
    {
        var data = await _cachedService.GetCacheAsync<List<UserResponse>>("Users", cancellationToken);


        if (data is null)
        {
            data = await _context.Users
                .Select(x=> new UserResponse(x.UserId, x.Username, x.Email, x.EmailConfirmed,x.PhoneNumber))
                .AsNoTracking()
                .ToListAsync();
        }

        if (data is not null)
        {
            await _cachedService.AddCacheAsync<List<UserResponse>>("Users", data, cancellationToken);
        }
        
        return data;
    }

    /// <inheritdoc/>
    public async Task<UserResponse?> UpdateUserByIdAsync(UserUpdateRequest request, CancellationToken cancellationToken)
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

        await _cachedService.RemoveCacheAsync(request.Id.ToString(), cancellationToken);

        return new UserResponse(user.UserId, user.Username, user.Email, user.EmailConfirmed, user.PhoneNumber);
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteUserByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);
        
        if (user is null)
        {
            return false;
        }
        
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        await _cachedService.RemoveCacheAsync(id.ToString(), cancellationToken);
        
        return true;
    }
    
    /// <inheritdoc/>
    public async Task<bool> IsEmailUniqueAsync(string email)
    {
        return !await _context.Users.AnyAsync(x => x.Email == email);
    }
    
    /// <inheritdoc/>
    public async Task<bool> IsPhoneNumberUniqueAsync(string phoneNumber)
    {
        return !await _context.Users.AnyAsync(x=> x.PhoneNumber == phoneNumber);
    }

    /// <inheritdoc/>
    public async Task<bool> IsUsernameUniqueAsync(string userName)
    {
        return !await _context.Users.AnyAsync(x => x.Username == userName);
    }

    /// <inheritdoc/>
    public async Task<bool> IsUserExist(Guid userId)
    {
        return await _context.Users.AnyAsync(x => x.UserId == userId);
    }
}