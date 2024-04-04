namespace WorkerServicePoc.Application.Messaging;

public sealed class MessageBrokerSettings
{
    public const string Section = "MessageBroker";

    public string Host { get; init; } = string.Empty;
    public string Username { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}
