namespace UserService.Domain.Contracts;

public record UserResponse(Guid Id, string Username, string Email, bool EmailConfirmed, string PhoneNumber);