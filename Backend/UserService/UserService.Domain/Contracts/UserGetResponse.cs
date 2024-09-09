namespace UserService.Domain.Contracts;

public record UserGetResponse(Guid UserId, string Username, string Password, string Email, bool EmailConfirmed, string PhoneNumber);