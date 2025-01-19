using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using UserService.Domain.Interfaces;
using UserService.Domain.Models;
using UserService.Infrastructure.Context;

namespace UserService.Infrastructure.Repository;

public class UserRepository(ApplicationDbContext context) : IUserRepository
{
    private readonly ApplicationDbContext _context = context;
    
    public async Task<User?> AddUserAsync(User user, CancellationToken cancellationToken)
    {
        var result = await _context.Users.AddAsync(user, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return result.Entity;
    }

    public async Task<User?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Include(x=> x.Role)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.UserId == id && !x.IsDelete, cancellationToken);

        if (user is null)
        {
            return null;
        }

        return user;
    }

    public async Task<IReadOnlyCollection<User>> GetUsersAsync(CancellationToken cancellationToken)
    {
        var users = await _context.Users
            .Include(x=> x.Role)
            .Where(x=> !x.IsDelete)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return users;
    }

    public async Task<User?> UpdateUserByIdAsync(Guid id, User user, CancellationToken cancellationToken)
    {
        var updatingEntity = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id, cancellationToken);

        if (updatingEntity is null)
        {
            return null;
        }

        updatingEntity.UserName = user.UserName;
        updatingEntity.Email = user.Email;
        updatingEntity.PhoneNumber = user.PhoneNumber;

        var result = _context.Users.Update(updatingEntity);
        await _context.SaveChangesAsync(cancellationToken);

        return result.Entity;
    }

    public async Task<User?> DeleteUserByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var deletingUser =
            await _context.Users.FirstOrDefaultAsync(x => x.UserId == id && !x.IsDelete, cancellationToken);

        if (deletingUser is null)
        {
            return null;
        }

        deletingUser.IsDelete = true;

        var result = _context.Users.Update(deletingUser);
        await _context.SaveChangesAsync(cancellationToken);

        return result.Entity;
    }

    public async Task<User?> RestoreUserByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var restoringUser =
            await _context.Users.FirstOrDefaultAsync(x => x.UserId == id && x.IsDelete == true, cancellationToken);

        if (restoringUser is null)
        {
            return null;
        }

        restoringUser.IsDelete = false;

        var result = _context.Users.Update(restoringUser);

        await _context.SaveChangesAsync(cancellationToken);
        
        return result.Entity;
    }

    public async Task<bool> ExistByEmail(string? email)
    {
        return await _context.Users.AnyAsync(x => x.Email == email);
    }

    public async Task<bool> ExistByPhoneNumber(string? phoneNumber)
    {
        return await _context.Users.AnyAsync(x => x.PhoneNumber == phoneNumber);
    }

    public async Task<bool> ExistByUserName(string userName)
    {
        return await _context.Users.AnyAsync(x => x.UserName == userName);
    }
}