namespace UserService.Domain.DTO;

public record UserAddRequest(string UserName, string Password, string Email, string PhoneNumber);