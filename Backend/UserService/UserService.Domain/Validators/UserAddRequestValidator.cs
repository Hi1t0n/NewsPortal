using FluentValidation;
using UserService.Domain.Contracts;
using UserService.Domain.Interfaces;

namespace UserService.Domain.Validators;

public sealed class 
    UserAddRequestValidator : AbstractValidator<UserAddRequest>
{
    private readonly IUserService _userService;
    
    public UserAddRequestValidator(IUserService userService)
    {
        _userService = userService;
        
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("Username is required")
            .MaximumLength(255).WithMessage("Username must not exceed 255 characters")
            .MustAsync(BeUniqueUsername).WithMessage("Username already exists");
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters")
            .MaximumLength(255).WithMessage("Password must not exceed 255 characters");
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .MustAsync(BeUniqueEmail).WithMessage("Email already exists");
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required")
            .MustAsync(BeUniquePhoneNumber).WithMessage("Phone number already exists");
    }

    private async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
    {
        return await _userService.IsEmailUniqueAsync(email);
    }

    private async Task<bool> BeUniquePhoneNumber(string phoneNumber, CancellationToken cancellationToken)
    {
        return await _userService.IsPhoneNumberUniqueAsync(phoneNumber);
    }

    private async Task<bool> BeUniqueUsername(string username, CancellationToken cancellationToken)
    {
        return await _userService.IsUsernameUniqueAsync(username);
    }
}