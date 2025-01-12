using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserService.Domain;
using UserService.Infrastructure.Context;

namespace UserService.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBusinessLogic(this IServiceCollection serviceCollection, IConfiguration  configuration, string connectionString)
    {
        serviceCollection.AddDataBase(connectionString);
        return serviceCollection;
    }

    private static IServiceCollection AddDataBase(this IServiceCollection serviceCollection, string connectionString)
    {
        serviceCollection.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString, 
                builder => builder.EnableRetryOnFailure(Constants.RetryOnFailure)));

        return serviceCollection;
    }
}