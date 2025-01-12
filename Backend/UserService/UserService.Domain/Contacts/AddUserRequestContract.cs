namespace UserService.Domain.Contacts;

public record AddUserRequestContract(string UserName, string Password, string? Email, string? PhoneNumber);