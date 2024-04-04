using WorkerServicePoc.Domain.Outbox;

namespace WorkerServicePoc.Application.PublishOutbox.Extensions;

public static class OutboxMessagesExtensions
{
    public static OutboxMessageCreatedIntegrationEvent ToIntegrationEvent(this OutboxMessage outboxMessage)
        => new(Guid.NewGuid(), outboxMessage.Id);
}
