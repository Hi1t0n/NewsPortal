using UserService.Domain.Contacts;
using UserService.Domain.Models;
using UserService.Infrastructure.Services;

namespace UserService.Infrastructure.Extensions;

public static class ContractExtensions
{
    public static User ToModel(this AddUserRequestContract contract)
    {
        var user = new User()
        {
            UserId = Guid.NewGuid(),
            UserName = contract.UserName.ToLower(),
            Password = CryptoService.HashPassword(contract.Password),
            Email = contract.Email!.ToLower(),
            PhoneNumber = contract.PhoneNumber
        };

        return user;
    }

    public static User ToModel(this UpdateUserRequestContract contract)
    {
        var user = new User()
        {
            UserName = contract.UserName.ToLower(),
            Email = contract.Email.ToLower(),
            PhoneNumber = contract.PhoneNumber
        };

        return user;
    }
}