using FluentValidation;
using UserService.Domain.Interfaces;

namespace UserService.Infrastructure.Services;

public class ValidatorService : IValidatorService
{
    private readonly IUserService _userService;
    public ValidatorService(IUserService userService)
    {
        _userService = userService;
    }
    
    public async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
    {
        return await _userService.IsEmailUniqueAsync(email);
    }

    public async Task<bool> BeUniquePhoneNumber(string phoneNumber, CancellationToken cancellationToken)
    {
        return await _userService.IsPhoneNumberUniqueAsync(phoneNumber);
    }

    public async Task<bool> BeUniqueUsername(string username, CancellationToken cancellationToken)
    {
        return await _userService.IsUsernameUniqueAsync(username);
    }

    public async Task<bool> BeUserExist(Guid id, CancellationToken cancellationToken)
    {
        return await _userService.IsUserExist(id);
    }
}