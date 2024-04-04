namespace WorkerServicePoc.Domain.Outbox;

public enum OutboxState
{
    Created,
    Published,
    Failed
}
