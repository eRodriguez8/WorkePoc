﻿namespace WorkerServicePoc.Application.Abstractions.Events;

public interface IIntegrationEvent
{
    Guid Id { get; init; }
}
