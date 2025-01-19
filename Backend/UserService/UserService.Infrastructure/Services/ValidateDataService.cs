using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using UserService.Domain;
using UserService.Domain.Interfaces;
using UserService.Domain.Models;

namespace UserService.Infrastructure.Services;

public static class ValidateDataService
{
    
    public static async Task<ValidateResult> ValidateUserData(this User user, IUserRepository userRepository)
    {
        if(string.IsNullOrWhiteSpace(user.UserName))
        {
            return ValidateResult.Invalid(HttpStatusCode.BadRequest, $"Incorrect Username");
        }

        if (await userRepository.ExistByUserName(user.UserName.ToLower()))
        {
            return  ValidateResult.Invalid(HttpStatusCode.BadRequest, $"Username is already taken");
        }
        
        if(string.IsNullOrWhiteSpace(user.Password))
        {
            return ValidateResult.Invalid(HttpStatusCode.BadRequest, $"Incorrect Password");
        }

        if(!IsValidEmail(user.Email))
        {
            return ValidateResult.Invalid(HttpStatusCode.BadRequest, $"Incorrect Email");
        }
        
        if (await userRepository.ExistByEmail(user.Email!.ToLower()))
        {
            return ValidateResult.Invalid(HttpStatusCode.Conflict, $"Email is already taken");
        }

        if (!IsValidPhoneNumber(user.PhoneNumber))
        {
            return ValidateResult.Invalid(HttpStatusCode.BadRequest, $"Incorrect phone number");
        }
        
        if (await userRepository.ExistByPhoneNumber(user.PhoneNumber))
        {
            return ValidateResult.Invalid(HttpStatusCode.Conflict, $"Phone number is already taken");
        }
        
        
        return ValidateResult.Valid();
    }
    
    /// <summary>
    /// Проверка email на корректность в соответствии с RFC 5322
    /// </summary>
    /// <param name="email">Электронная почта</param>
    /// <returns>Результат проверки</returns>
    private static bool IsValidEmail(string? email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return false;
        }

        try
        {
            var mailAddress = new MailAddress(email);
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    private static bool IsValidPhoneNumber(string? phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
        {
            return false;
        }
        
        string phoneRegex = @"^\+?[1-9]\d{0,2}[-.\s]?(\(?\d{1,4}\)?[-.\s]?)*\d{4,10}$";
        return Regex.IsMatch(phoneNumber, phoneRegex);
    }
}