using UserService.Domain.Contracts;

namespace UserService.Domain.Interfaces;

public interface IUserService
{ 
    Task AddUserAsync(UserAddRequest request);
    Task<UserGetResponse> GetUserByIdAsync(Guid id);
    Task<List<UserGetResponse>> GetUsersAsync();
    Task<bool> IsEmailUniqueAsync(string email);
    Task<bool> IsPhoneNumberUniqueAsync(string phoneNumber);
    Task<bool> IsUsernameUniqueAsync(string userName);
}