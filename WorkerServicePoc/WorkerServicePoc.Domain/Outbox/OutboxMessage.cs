namespace WorkerServicePoc.Domain.Outbox;

public sealed class OutboxMessage
{
    public Guid Id { get; private set; }
    public string Type { get; private set; }
    public string Content { get; private set; }
    public OutboxState State { get; private set; }
    public DateTime OcurredOnUtc { get; private set; }
    public DateTime? ProcessedOnUtc { get; private set; }
    public string? Error { get; private set; }

    private OutboxMessage() { } // For EF Core

    public static OutboxMessage Create(string type, string content)
    {
        return new()
        {
            Id = Guid.NewGuid(),
            Type = type,
            Content = content,
            State = OutboxState.Created,
            OcurredOnUtc = DateTime.UtcNow,
            ProcessedOnUtc = null,
            Error = null
        };
    }

    public bool HasProcessedOnUtc() => ProcessedOnUtc is not null;

    public void AdvanceState()
    {
        if (State is OutboxState.Published)
        {
            return;
        }

        ProcessedOnUtc = DateTime.UtcNow;
        State = OutboxState.Published;
        Error = string.Empty;
    }

    public void SetError(string error)
    {
        Error = error;
        State = OutboxState.Failed;
    }
}