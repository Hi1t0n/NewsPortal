using Microsoft.Extensions.DependencyInjection;
using PostService.Domain.Constants;
using PostService.Infrastructure.Context;

namespace PostService.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBusinessLogic(this IServiceCollection serviceCollection,
        string connectionStringDb)
    {
        serviceCollection.AddDatabase(connectionStringDb);
        serviceCollection.AddService();

        return serviceCollection;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection serviceCollection, string connectionString)
    {
        serviceCollection.AddNpgsql<ApplicationDbContext>(connectionString, builder =>
        {
            builder.EnableRetryOnFailure(DatabaseConfig.RetryOnFailure);
        } );

        return serviceCollection;
    }

    private static IServiceCollection AddService(this IServiceCollection serviceCollection)
    {
        return serviceCollection;
    }
}