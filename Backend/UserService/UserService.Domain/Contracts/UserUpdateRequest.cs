namespace UserService.Domain.Contracts;

/// <summary>
/// DTO для обновления пользователя
/// </summary>
/// <param name="Id">Идентификатор</param>
/// <param name="Username">Имя пользователя</param>
/// <param name="Password">Пароль</param>
/// <param name="Email">Электронная почта</param>
/// <param name="PhoneNumber">Номер телефона</param>
public record UserUpdateRequest(Guid Id ,string Username, string Password, string Email, string PhoneNumber);