using Microsoft.EntityFrameworkCore;
using WorkerServicePoc.Persistance;

namespace WorkerServicePoc.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IHost host)
    {
        using IServiceScope scope = host.Services.CreateScope();
        IServiceProvider services = scope.ServiceProvider;

        try
        {
            var dbContext = services.GetRequiredService<WorkerDbContext>();
            dbContext.Database.Migrate();
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<WorkerDbContext>>();
            logger.LogError(ex, "An error occurred while migrating the database.");
        }
    }
}
