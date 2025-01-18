namespace UserService.Domain.Contacts;

public record UpdateUserRequestContract(string UserName, string Email, string PhoneNumber);