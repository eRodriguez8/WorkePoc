using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorkerServicePoc.Domain.Abstractions;
using WorkerServicePoc.Domain.Outbox;
using WorkerServicePoc.Persistance.Outbox;

namespace WorkerServicePoc.Persistance;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("PostgresDB") ?? throw new ArgumentNullException(nameof(configuration));

        services.AddDbContext<WorkerDbContext>(options => options.UseNpgsql(connectionString));

        services.AddScoped<IOutboxRepository, OutboxRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<WorkerDbContext>());

        return services;
    }
}
