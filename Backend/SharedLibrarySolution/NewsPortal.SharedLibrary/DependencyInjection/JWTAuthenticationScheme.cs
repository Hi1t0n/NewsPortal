using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace SharedLibrary.DependencyInjection;

public static class JwtAuthenticationScheme
{
    /// <summary>
    /// Регистрация схемы аутентификации JWT
    /// </summary>
    /// <param name="serviceCollection">Контейнер служб <see cref="IServiceCollection"/></param>
    /// <param name="configuration">Конфигурация приложения <see cref="IConfiguration"/></param>
    /// <returns>Модифицированный контейнер служб <see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddJwtAuthenticationScheme(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                var key = Encoding.UTF8.GetBytes(configuration.GetSection("Authentication:Key").Value!);
                string issuer = configuration.GetSection("Authentication:Issuer").Value!;
                string audience = configuration.GetSection("Authentication:Audience").Value!;

                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });
        return serviceCollection;
    }
}