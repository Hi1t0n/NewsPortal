using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
    /// <param name="connectionString">Строка подключения</param>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddBusinessLogic(this IServiceCollection serviceCollection,
        string connectionString)
    {
        serviceCollection.AddService();
        serviceCollection.AddDatabase(connectionString);
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
        return serviceCollection;
    }
    
    /// <summary>
    /// Добавление БД в приложение
    /// </summary>
    /// <param name="serviceCollection">Класс для расширения <see cref="IServiceCollection"/></param>
    /// <param name="connectionString">Строка подключения</param>
    /// <returns>Обновленая коллекция <see cref="IServiceCollection"/></returns>
    private static IServiceCollection AddDatabase(this IServiceCollection serviceCollection, string connectionString)
    {
        serviceCollection.AddDbContext<ApplicationDbContext>(x=> x.UseNpgsql(connectionString));
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