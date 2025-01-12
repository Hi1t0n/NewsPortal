using UserService.Domain.Contacts;
using UserService.Domain.Models;

namespace UserService.Infrastructure.Extensions;

public static class ContractExtensions
{
    public static User ToModel(this AddUserRequestContract contract)
    {
        var user = new User()
        {
            UserId = Guid.NewGuid(),
            UserName = contract.UserName,
            Password = contract.Password,
            Email = contract.Email,
            PhoneNumber = contract.PhoneNumber
        };

        return user;
    }
}