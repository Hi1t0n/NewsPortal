using UserService.Domain.DTO;

namespace UserService.Domain.Interfaces;

public interface IUserManager
{ 
    void AddUserAsync(UserAddRequest request);
    Task<UserGetResponse> GetUserByIdAsync(Guid id);
    Task<List<UserGetResponse>> GetUsersAsync();
}