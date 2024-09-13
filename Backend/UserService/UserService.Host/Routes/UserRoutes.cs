using FluentValidation;
using UserService.Domain.Contracts;
using UserService.Domain.Interfaces;
using UserService.Domain.Validators;
using UserService.Host.Models;

namespace UserService.Host.Routes;

public static class UserRoutes
{
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

    public static async Task<IResult> AddUser(UserAddRequest request, IUserRepository userRepository,
        UserAddRequestValidator validator, CancellationToken cancellation)
    {
        var result = await validator.ValidateAsync(request);
        if (!result.IsValid)
        {
            return Results.BadRequest(result.Errors);
        }
        var user = await userRepository.AddUserAsync(request);
        
        return Results.Created($"/api/users/{user.UserId}",new UserResponse(user.UserId, user.Username, user.Email, user.EmailConfirmed, user.PhoneNumber));
    }

    public static async Task<IResult> GetUsers(IUserRepository userRepository, CancellationToken cancellationToken)
    {
        var users = await userRepository.GetUsersAsync();
        return Results.Ok(users);
    }

    public static async Task<IResult> GetUserById(Guid id, IUserRepository userRepository, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserByIdAsync(id);
        if (user is null)
        {
            return Results.NotFound(new
            {
                errorMessage = $"User with {id} was not found"
            });
        }
        
        return Results.Ok(user);
    }

    public static async Task<IResult> UpdateUserById(UserUpdateRequest request, IUserRepository userRepository,
        UserUpdateRequestValidator validator, CancellationToken cancellationToken)
    {
        var result = await validator.ValidateAsync(request);
        
        if (!result.IsValid)
        {
            return Results.BadRequest(result.Errors);
        }
        
        var user = await userRepository.UpdateUserByIdAsync(request);

        if (user is null)
        {
            return Results.NotFound($"User with Id: {request.Id} was not found");
        }
        
        return Results.Ok(user);
    }

    public static async Task<IResult> DeleteUserById(Guid id, IUserRepository userRepository, CancellationToken cancellationToken)
    {
        var result = await userRepository.DeleteUserByIdAsync(id);
        
        return result ? Results.Ok() : Results.NotFound($"User with Id: {id} was not found");
    }
}