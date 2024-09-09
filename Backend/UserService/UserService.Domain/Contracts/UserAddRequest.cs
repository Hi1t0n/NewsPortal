namespace UserService.Domain.Contracts;

public record UserAddRequest(string UserName, string Password, string Email, string PhoneNumber);