using Microsoft.Extensions.DependencyInjection;
using WorkerServicePoc.Application.Abstractions.Commands;
using WorkerServicePoc.Application.PublishOutbox;

namespace WorkerServicePoc.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICommandHandler, PubishOutboxMessagesHandler>();

        return services;
    }
}
