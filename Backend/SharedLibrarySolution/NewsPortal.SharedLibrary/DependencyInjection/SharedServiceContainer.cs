using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using SharedLibrary.Classes;
using SharedLibrary.Middleware;

namespace SharedLibrary.DependencyInjection;

public static class SharedServiceContainer
{
    /// <summary>
    /// Регистрация всех служб
    /// </summary>
    /// <param name="serviceCollection">Контейнер служб <see cref="IServiceCollection"/></param>
    /// <param name="configuration">Конфигурация приложения <see cref="IConfiguration"/></param>
    /// <param name="connectionString">Строка подключения</param>
    /// <param name="fileName">Имя файла</param>
    /// <typeparam name="TContext">Тип контекста базы данных унаследованного от <see cref="DbContext"/></typeparam>
    /// <returns>Модифицированный контейнер служб <see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddSharedService<TContext>(this IServiceCollection serviceCollection,
        IConfiguration configuration, string connectionString, string fileName) where TContext : DbContext
    {
        serviceCollection.AddDatabase<TContext>(connectionString);
        serviceCollection.AddSerilogLogging(fileName);
        serviceCollection.AddJwtAuthenticationScheme(configuration);
        
        return serviceCollection;
    }

    /// <summary>
    /// Регистрируем контекст базы данных в службах
    /// </summary>
    /// <param name="serviceCollection">Контейнер служб <see cref="IServiceCollection"/></param>
    /// <param name="connectionString">Строка подключения к базе данных</param>
    /// <typeparam name="TContext">Тип контекста базы данных унаследованного от <see cref="DbContext"/></typeparam>
    /// <returns>Модифицированный контейнер служб<see cref="IServiceCollection"/></returns>
    private static IServiceCollection AddDatabase<TContext>(this IServiceCollection serviceCollection,
        string connectionString) where TContext : DbContext
    {
        serviceCollection.AddDbContext<TContext>(options => 
            options.UseNpgsql(connectionString, 
                npgOption => {
            npgOption.EnableRetryOnFailure(Constants.TryConnectOfError);
        }));

        return serviceCollection;
    }
    
    /// <summary>
    /// Регистрируем логгер в службах
    /// </summary>
    /// <param name="serviceCollection">Контейнер служб <see cref="IServiceCollection"/></param>
    /// <param name="fileName">Имя файла</param>
    /// <returns>Модифицированный контейнер служб <see cref="IServiceCollection"/></returns>
    private static IServiceCollection AddSerilogLogging(this IServiceCollection serviceCollection, string fileName)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Debug()
            .WriteTo.Console()
            .WriteTo.File(path: $"{fileName}-.text",
                restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information,
                outputTemplate: "{{Timestamp:dd-MM-yyyy HH:mm:ss.fff zzz}} [{{Level:u3}}] {{message:lj}}{{NewLine}}{Exception}",
                rollingInterval: RollingInterval.Day)
            .CreateLogger();
        return serviceCollection;
    }

    public static IApplicationBuilder AddMiddleware(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseMiddleware<GlobalException>();
        applicationBuilder.UseMiddleware<ListenToOnlyApiGateway>();

        return applicationBuilder;
    }
}