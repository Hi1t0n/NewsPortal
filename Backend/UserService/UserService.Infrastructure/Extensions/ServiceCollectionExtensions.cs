using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedLibrary.DependencyInjection;
using UserService.Domain.Interfaces;
using UserService.Domain.Validators;
using UserService.Infrastructure.Context;
using UserService.Infrastructure.Repositories;
using UserService.Infrastructure.Services;

namespace UserService.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Добавление бизнес логики
    /// </summary>
    /// <param name="serviceCollection">Класс расширения <see cref="IServiceCollection"/></param>
    /// <param name="configuration"><see cref="IConfiguration"/></param>
    /// <param name="connectionString">Строка подключения</param>
    /// <param name="connectionStringRedis">Строка подключения к Redis</param>
    /// <param name="fileName">Название файла</param>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddBusinessLogic(this IServiceCollection serviceCollection,
        IConfiguration configuration, string connectionString, string connectionStringRedis, string fileName)
    {
        serviceCollection.AddSharedService<ApplicationDbContext>(configuration, connectionString, fileName);
        serviceCollection.AddService();
        serviceCollection.AddDatabase(connectionStringRedis);
        serviceCollection.AddValidator();
        return serviceCollection;
    }
    
    /// <summary>
    /// Добавление сервисов в приложение
    /// </summary>
    /// <param name="serviceCollection">Класс для расширения<see cref="IServiceCollection"/></param>
    /// <returns>Обновленая коллекция <see cref="IServiceCollection"/></returns>
    private static IServiceCollection AddService(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IUserRepository, UserRepository>();
        serviceCollection.AddScoped<IValidatorService, ValidatorService>();
        serviceCollection.AddScoped<ICryptoService, CryptoService>();
        serviceCollection.AddScoped<ICachedService, CachedService>();
        return serviceCollection;
    }

    /// <summary>
    /// Добавление БД в приложение
    /// </summary>
    /// <param name="serviceCollection">Класс для расширения <see cref="IServiceCollection"/></param>
    /// <param name="connectionStringRedis">Строка подключения к Redis</param>
    /// <returns>Обновленая коллекция <see cref="IServiceCollection"/></returns>
    private static IServiceCollection AddDatabase(this IServiceCollection serviceCollection, string connectionStringRedis)
    {
        serviceCollection.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = connectionStringRedis;
            options.InstanceName = "UserService";
        });
        
        return serviceCollection;
    }
    
    /// <summary>
    /// Добавление валидаторов
    /// </summary>
    /// <param name="serviceCollection">Класс для расширения <see cref="IServiceCollection"/></param>
    /// <returns>Обновленная коллекция <see cref="IServiceCollection"/></returns>
    private static IServiceCollection AddValidator(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddValidatorsFromAssemblyContaining(typeof(UserAddRequestValidator));
        serviceCollection.AddValidatorsFromAssemblyContaining(typeof(UserUpdateRequestValidator));
        
        return serviceCollection;
    }
}