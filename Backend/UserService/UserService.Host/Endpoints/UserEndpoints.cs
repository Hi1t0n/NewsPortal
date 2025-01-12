using UserService.Domain.Contacts;
using UserService.Domain.Interfaces;
using UserService.Infrastructure.Extensions;

namespace UserService.Host.Endpoints;

public static class UserEndpoints
{
    public static WebApplication AddUserEndpoints(this WebApplication webApplication)
    {
        webApplication.MapGroup("/api/users/");
        webApplication.MapPost(pattern: "/", handler: AddUserAsync);

        return webApplication;
    }

    private async static Task<IResult> AddUserAsync(AddUserRequestContract contract, IUserRepository repository)
    {
        var user = contract.ToModel();

        var result = await repository.AddUserAsync(user);

        if (result is null)
        {
            return Results.BadRequest();
        }

        return Results.Ok();
    } 
}