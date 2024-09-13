using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UserService.Domain.Interfaces;
using UserService.Infrastructure.Context;
using UserService.Infrastructure.Repositories;
using UserService.Infrastructure.Services;

namespace UserService.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBusinessLogic(this IServiceCollection serviceCollection,
        string connectionString)
    {
        serviceCollection.AddService();
        serviceCollection.AddDatabase(connectionString);
        return serviceCollection;
    }

    private static IServiceCollection AddService(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IUserRepository, UserRepository>();
        serviceCollection.AddScoped<IValidatorService, ValidatorService>();
        return serviceCollection;
    }
    
    private static IServiceCollection AddDatabase(this IServiceCollection serviceCollection, string connectionString)
    {
        serviceCollection.AddDbContext<ApplicationDbContext>(x=> x.UseNpgsql(connectionString));
        return serviceCollection;
    }
}