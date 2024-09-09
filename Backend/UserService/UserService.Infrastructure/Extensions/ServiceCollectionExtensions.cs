using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UserService.Domain.Interfaces;
using UserService.Infrastructure.Context;

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
        serviceCollection.AddScoped<IUserService, Services.UserService>();
        return serviceCollection;
    }
    
    private static IServiceCollection AddDatabase(this IServiceCollection serviceCollection, string connectionString)
    {
        serviceCollection.AddDbContext<ApplicationDbContext>(x=> x.UseNpgsql(connectionString));
        return serviceCollection;
    }
}