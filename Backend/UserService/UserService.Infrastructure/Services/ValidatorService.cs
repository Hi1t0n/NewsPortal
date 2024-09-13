using FluentValidation;
using UserService.Domain.Interfaces;

namespace UserService.Infrastructure.Services;

public class ValidatorService : IValidatorService
{
    private readonly IUserRepository _userRepository;
    public ValidatorService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
    {
        return await _userRepository.IsEmailUniqueAsync(email);
    }

    public async Task<bool> BeUniquePhoneNumber(string phoneNumber, CancellationToken cancellationToken)
    {
        return await _userRepository.IsPhoneNumberUniqueAsync(phoneNumber);
    }

    public async Task<bool> BeUniqueUsername(string username, CancellationToken cancellationToken)
    {
        return await _userRepository.IsUsernameUniqueAsync(username);
    }

    public async Task<bool> BeUserExist(Guid id, CancellationToken cancellationToken)
    {
        return await _userRepository.IsUserExist(id);
    }
}