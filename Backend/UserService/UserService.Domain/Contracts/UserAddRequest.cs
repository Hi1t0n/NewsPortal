namespace UserService.Domain.Contracts;

/// <summary>
/// DTO для добавления пользователя
/// </summary>
/// <param name="UserName">Имя пользователя</param>
/// <param name="Password">Пароль</param>
/// <param name="Email">Электронная почта</param>
/// <param name="PhoneNumber">Номер телефона</param>
public record UserAddRequest(string UserName, string Password, string Email, string PhoneNumber);