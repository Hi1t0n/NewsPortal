using UserService.Domain.Contracts;
using UserService.Domain.Interfaces;
using UserService.Domain.Validators;

namespace UserService.Host.Routes;

/// <summary>
/// Роутер для работы с пользователем
/// </summary>
public static class UserRoutes
{
    /// <summary>
    /// Метод для добавления роутера
    /// </summary>
    /// <param name="application">Объект приложения <see cref="WebApplication"/></param>
    /// <returns>Модифицированный объект приложения <see cref="WebApplication"/></returns>
    public static WebApplication AddUserRouters(this WebApplication application)
    {
        var userGroup = application.MapGroup("api/users");
        userGroup.MapPost(pattern: "/", handler: AddUser);
        userGroup.MapGet(pattern:"/", handler: GetUsers);
        userGroup.MapGet(pattern:"/{id:guid}", handler: GetUserById);
        userGroup.MapPut(pattern: "/{id:guid}", handler: UpdateUserById);
        userGroup.MapDelete(pattern: "/{id:guid}", handler: DeleteUserById);
        return application;
    }

    /// <summary>
    /// Роутер добавления пользователя
    /// </summary>
    /// <param name="request">DTO с данными пользователя</param>
    /// <param name="userRepository"><see cref="IUserRepository"/></param>
    /// <param name="validator">Валидатор <see cref="UserAddRequestValidator"/></param>
    /// <param name="cancellation"><see cref="CancellationToken"/></param>
    /// <returns>Данные добавленного пользователя</returns>
    public static async Task<IResult> AddUser(UserAddRequest request, IUserRepository userRepository,
        UserAddRequestValidator validator, CancellationToken cancellationToken)
    {
        var result = await validator.ValidateAsync(request);
        if (!result.IsValid)
        {
            return Results.BadRequest(result.Errors);
        }
        var user = await userRepository.AddUserAsync(request, cancellationToken);
        
        return Results.Created($"/api/users/{user.UserId}",new UserResponse(user.UserId, user.Username, user.Email, user.EmailConfirmed, user.PhoneNumber));
    }
    
    /// <summary>
    /// Роутер получения всех пользователей
    /// </summary>
    /// <param name="userRepository"><see cref="IUserRepository"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Список всех пользователей</returns>
    public static async Task<IResult> GetUsers(IUserRepository userRepository, CancellationToken cancellationToken)
    {
        var users = await userRepository.GetUsersAsync(cancellationToken);
        return Results.Ok(users);
    }
    
    /// <summary>
    /// Роутер получения пользователя по Id
    /// </summary>
    /// <param name="id">Идентификатор пользователя</param>
    /// <param name="userRepository"><see cref="IUserRepository"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Данные пользователя</returns>
    public static async Task<IResult> GetUserById(Guid id, IUserRepository userRepository, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserByIdAsync(id, cancellationToken);
        if (user is null)
        {
            return Results.NotFound(new
            {
                errorMessage = $"User with {id} was not found"
            });
        }
        
        return Results.Ok(user);
    }
    
    /// <summary>
    /// Роутер обновления пользователя по Id
    /// </summary>
    /// <param name="request">DTO c новыми данными</param>
    /// <param name="userRepository"><see cref="IUserRepository"/></param>
    /// <param name="validator">Валидатор<see cref="UserUpdateRequestValidator"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Данные обновленного пользователя</returns>
    public static async Task<IResult> UpdateUserById(UserUpdateRequest request, IUserRepository userRepository,
        UserUpdateRequestValidator validator, CancellationToken cancellationToken)
    {
        var result = await validator.ValidateAsync(request);
        
        if (!result.IsValid)
        {
            return Results.BadRequest(result.Errors);
        }
        
        var user = await userRepository.UpdateUserByIdAsync(request, cancellationToken);

        if (user is null)
        {
            return Results.NotFound($"User with Id: {request.Id} was not found");
        }
        
        return Results.Ok(user);
    }
    
    /// <summary>
    /// Роутер удаления пользователя по Id
    /// </summary>
    /// <param name="id">Идентификатор пользователя</param>
    /// <param name="userRepository"><see cref="IUserRepository"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Результат удаления</returns>
    public static async Task<IResult> DeleteUserById(Guid id, IUserRepository userRepository, CancellationToken cancellationToken)
    {
        var result = await userRepository.DeleteUserByIdAsync(id, cancellationToken);
        
        return result ? Results.Ok(result) : Results.NotFound($"User with Id: {id} was not found");
    }
}