using MassTransit;
using WorkerServicePoc.Application.Abstractions.Events;

namespace WorkerServicePoc.Infrastructure.MessageBroker;

internal sealed class EventBus(IPublishEndpoint publishEndpoint) : IEventBus
{
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

    public async Task PublishAsync<T>(T integrationEvent, CancellationToken cancellationToken) where T : class, IIntegrationEvent
    {
        await _publishEndpoint.Publish(integrationEvent, cancellationToken);
    }
}
