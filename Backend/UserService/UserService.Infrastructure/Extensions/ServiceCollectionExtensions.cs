using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserService.Domain;
using UserService.Domain.Interfaces;
using UserService.Infrastructure.Context;
using UserService.Infrastructure.Repository;

namespace UserService.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBusinessLogic(this IServiceCollection serviceCollection, string connectionStringDb, string connectionStringRedis)
    {
        serviceCollection.AddServices();
        serviceCollection.AddDataBase(connectionStringDb);
        serviceCollection.AddRedis(connectionStringRedis);
        
        return serviceCollection;
    }

    private static IServiceCollection AddDataBase(this IServiceCollection serviceCollection, string connectionString)
    {
        serviceCollection.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString, 
                builder =>
                {
                    builder.EnableRetryOnFailure(Constants.RetryOnFailure);
                }));

        return serviceCollection;
    }

    private static IServiceCollection AddRedis(this IServiceCollection serviceCollection, string connectionString)
    {
        serviceCollection.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = connectionString;
            options.InstanceName = Constants.InstanceNameRedis;
        });

        return serviceCollection;
    }

    private static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IUserRepository, UserRepository>();

        return serviceCollection;
    }
}