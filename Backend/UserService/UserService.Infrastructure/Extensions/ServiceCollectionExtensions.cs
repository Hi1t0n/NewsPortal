using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserService.Infrastructure.Context;

namespace UserService.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBusinessLogic(this IServiceCollection serviceCollection,
        string connectionString)
    {
        serviceCollection.AddManager();
        serviceCollection.AddDatabase(connectionString);
        return serviceCollection;
    }

    private static IServiceCollection AddManager(this IServiceCollection serviceCollection)
    {
        return serviceCollection;
    }
    
    private static IServiceCollection AddDatabase(this IServiceCollection serviceCollection, string connectionString)
    {
        serviceCollection.AddDbContext<ApplicationDbContext>(x=> x.UseNpgsql(connectionString));
        return serviceCollection;
    }
}