namespace UserService.Domain.DTO;

public record UserGetResponse(Guid UserId, string Username, string Password, string Email, bool EmailConfirmed, string PhoneNumber);