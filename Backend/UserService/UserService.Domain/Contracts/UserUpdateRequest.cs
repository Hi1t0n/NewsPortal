namespace UserService.Domain.Contracts;

public record UserUpdateRequest(Guid Id ,string Username, string Password, string Email, string PhoneNumber);