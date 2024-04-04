using WorkerServicePoc.Application.Abstractions.Events;

namespace WorkerServicePoc.Application.PublishOutbox;

public sealed record OutboxMessageCreatedIntegrationEvent(Guid Id, Guid OutboxId) : IntegrationEvent(Id);
