using FluentValidation;
using UserService.Domain.Contracts;
using UserService.Domain.Interfaces;

namespace UserService.Domain.Validators;

public sealed class UserAddRequestValidator : AbstractValidator<UserAddRequest>
{
    private readonly IValidatorService _validatorService;
    
    public UserAddRequestValidator(IValidatorService validatorService)
    {
        _validatorService = validatorService;
        
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("Username is required")
            .MaximumLength(255).WithMessage("Username must not exceed 255 characters")
            .MustAsync(validatorService.BeUniqueUsername).WithMessage("Username already exists");
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters")
            .MaximumLength(255).WithMessage("Password must not exceed 255 characters");
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email address")
            .MustAsync(validatorService.BeUniqueEmail).WithMessage("Email already exists");
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required")
            .MustAsync(validatorService.BeUniquePhoneNumber).WithMessage("Phone number already exists");
    }
}