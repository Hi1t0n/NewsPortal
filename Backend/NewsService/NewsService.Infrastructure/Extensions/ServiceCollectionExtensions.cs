using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NewsService.Infrastructure.Context;

namespace NewsService.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBusinessLogic(this IServiceCollection servicesCollection, string connectionString)
    {
        servicesCollection.AddDataBase(connectionString);
        return servicesCollection;
    }

    public static IServiceCollection AddDataBase(this IServiceCollection servicesCollection, string connectionString)
    {
        servicesCollection.AddDbContext<ApplicationDbContext>(x=> x.UseNpgsql(connectionString));
        return servicesCollection;
    }

    public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        return serviceCollection;
    }
}