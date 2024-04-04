namespace WorkerServicePoc.Api.Endpoints.Outbox;

public sealed record CreateOutboxRequest(string Type, string Content);
