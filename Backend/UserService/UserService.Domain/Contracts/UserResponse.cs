namespace UserService.Domain.Contracts;

/// <summary>
/// DTO получения пользователя
/// </summary>
/// <param name="Id">Идентификатор</param>
/// <param name="Username">Имя пользователя</param>
/// <param name="Email">Электронная почта</param>
/// <param name="EmailConfirmed">Подтверждение почты</param>
/// <param name="PhoneNumber">Номер телефона</param>
public record UserResponse(Guid Id, string Username, string Email, bool EmailConfirmed, string PhoneNumber);