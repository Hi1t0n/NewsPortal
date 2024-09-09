using FluentValidation;
using UserService.Domain.Contracts;
using UserService.Domain.Interfaces;
using UserService.Domain.Validators;

namespace UserService.Host.Routes;

public static class UserRoutes
{
    public static WebApplication AddUserRouters(this WebApplication application)
    {
        var userGroup = application.MapGroup("api/users");
        userGroup.MapPost(pattern: "/", handler: AddUser);
        userGroup.MapGet(pattern:"/", handler: GetUsers);
        userGroup.MapGet(pattern:"/{id:guid}", handler: GetUserById);
        return application;
    }

    public static async Task<IResult> AddUser(UserAddRequest request, IUserService userService,
        UserAddRequestValidator validator, CancellationToken cancellation)
    {
        var result = await validator.ValidateAsync(request);
        if (!result.IsValid)
        {
            return Results.BadRequest(result.Errors);
        }
        await userService.AddUserAsync(request);
        
        return Results.Ok();
    }

    public static async Task<IResult> GetUsers(IUserService userService, CancellationToken cancellationToken)
    {
        var users = await userService.GetUsersAsync();
        return Results.Ok(users);
    }

    public static async Task<IResult> GetUserById(Guid id, IUserService userService, CancellationToken cancellationToken)
    {
        var user = await userService.GetUserByIdAsync(id);
        if (user is null)
        {
            return Results.NotFound(new
            {
                errorMessage = $"User with {id} was not found"
            });
        }
        
        return Results.Ok(user);
    }


}