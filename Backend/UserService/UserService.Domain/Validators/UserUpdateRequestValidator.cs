using FluentValidation;
using UserService.Domain.Contracts;
using UserService.Domain.Interfaces;

namespace UserService.Domain.Validators;

public sealed class UserUpdateRequestValidator : AbstractValidator<UserUpdateRequest>
{
    private readonly IValidatorService _validatorService;
    
    public UserUpdateRequestValidator(IValidatorService validatorService)
    {
        _validatorService = validatorService;
        RuleFor(x=> x.Id)
            .NotEmpty().WithMessage("Id cannot be empty")
            .MustAsync(_validatorService.BeUserExist).WithMessage(x=> $"User with Id: {x.Id} not exist");
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required")
            .MaximumLength(255).WithMessage("Username cannot be longer than 255 characters")
            .MustAsync(_validatorService.BeUniqueUsername).WithMessage("Username already exists");
        RuleFor(x=> x.Password)
            .NotEmpty().WithMessage("Password cannot be empty")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters")
            .MaximumLength(255).WithMessage("Password cannot be longer than 255 characters");
        RuleFor(x=> x.Email)
            .NotEmpty().WithMessage("Email cannot be empty")
            .EmailAddress().WithMessage("Invalid email address")
            .MustAsync(_validatorService.BeUniqueEmail).WithMessage("Email already exists");
        RuleFor(x=> x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number cannot be empty")
            .MustAsync(_validatorService.BeUniquePhoneNumber).WithMessage("Phone number already exists");
    }
}