using Microsoft.EntityFrameworkCore;
using UserService.Infrastructure.Context;

namespace UserService.Host.Extensions;

public static class WebApplicationExtensions
{
    public static void ApplyMigrations(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var pendingMigration =  context.Database.GetPendingMigrations();
            if (pendingMigration.Any())
            {
                context.Database.Migrate();
            }
        }
    }
}