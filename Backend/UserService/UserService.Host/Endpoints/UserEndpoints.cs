using UserService.Domain.Contacts;
using UserService.Domain.Interfaces;
using UserService.Domain.Models;
using UserService.Infrastructure.Extensions;

namespace UserService.Host.Endpoints;

public static class UserEndpoints
{
    public static WebApplication AddUserEndpoints(this WebApplication webApplication)
    {
        var mapGroup = webApplication.MapGroup("/api/users/");
        
        mapGroup.MapPost(pattern: "/", handler: AddUserAsync);
        mapGroup.MapGet(pattern: "/{userId:guid}", handler: GetUserByIdAsync);
        mapGroup.MapGet(pattern: "/", handler: GetUsersAsync);
        mapGroup.MapPut(pattern: "/{userId:guid}", handler: UpdateUserByIdAsync);
        mapGroup.MapDelete(pattern: "/{userId:guid}", handler: DeleteUserByIdAsync);
        mapGroup.MapPut(pattern: "/restore/{userId:guid}", handler: RestoreUserByIdAsync);
        
        return webApplication;
    }

    private static async Task<IResult> AddUserAsync(AddUserRequestContract contract, CancellationToken cancellationToken, IUserRepository repository)
    {
        var user = contract.ToModel();

        var result = await repository.AddUserAsync(user, cancellationToken);

        if (result is null)
        {
            return Results.BadRequest();
        }

        return Results.Ok();
    }

    private static async Task<IResult> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken,
        IUserRepository repository)
    {
        var result = await repository.GetUserByIdAsync(userId, cancellationToken);
        
        if(result is null)
        {
            return Results.NotFound(new
            {
                errorMessage = $"User with Id: {userId} Not Found"
            });
        }

        var response = result.ToResponse();

        return Results.Ok(response);
    }

    private static async Task<IResult> GetUsersAsync(CancellationToken cancellationToken, IUserRepository repository)
    {
        var result = await repository.GetUsersAsync(cancellationToken);

        var response = result.Select(x => x.ToResponse());

        return Results.Ok(response);
    }

    private static async Task<IResult> UpdateUserByIdAsync(Guid userId, UpdateUserRequestContract contract, CancellationToken cancellationToken,
        IUserRepository repository)
    {
        var user = contract.ToModel();
        
        var result = await repository.UpdateUserByIdAsync(userId, user, cancellationToken);

        if (result is null)
        {
            return Results.NotFound(new
            {
                errorMessage = $"User with Id: {userId} Not Found"
            });
        }

        return Results.Ok();
    }

    private static async Task<IResult> DeleteUserByIdAsync(Guid userId, CancellationToken cancellationToken,
        IUserRepository repository)

    {
        var result = await repository.DeleteUserByIdAsync(userId, cancellationToken);

        if (result is null)
        {
            return Results.NotFound(new
            {
                errorMessage = $"User with Id: {userId} Not Found"
            });
        }

        return Results.Ok();
    }

    private static async Task<IResult> RestoreUserByIdAsync(Guid userId, CancellationToken cancellationToken,
        IUserRepository repository)
    {
        var result = await repository.RestoreUserByIdAsync(userId, cancellationToken);
        
        if (result is null)
        {
            return Results.NotFound(new
            {
                errorMessage = $"User with Id: {userId} Not Found"
            });
        }

        return Results.Ok();
    }
}