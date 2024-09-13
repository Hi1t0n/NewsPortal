using UserService.Domain.Contracts;
using UserService.Host.Models;

namespace UserService.Domain.Interfaces;

public interface IUserRepository
{ 
    Task<User> AddUserAsync(UserAddRequest request);
    Task<UserGetResponse> GetUserByIdAsync(Guid id);
    Task<List<UserGetResponse>> GetUsersAsync();
    Task<UserResponse?> UpdateUserByIdAsync(UserUpdateRequest request);
    Task<bool> DeleteUserByIdAsync(Guid id);
    Task<bool> IsEmailUniqueAsync(string email);
    Task<bool> IsPhoneNumberUniqueAsync(string phoneNumber);
    Task<bool> IsUsernameUniqueAsync(string userName);
    Task<bool> IsUserExist(Guid userId);
}