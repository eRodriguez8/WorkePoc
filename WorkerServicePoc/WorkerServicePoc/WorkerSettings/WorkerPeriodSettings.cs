namespace WorkerServicePoc.WorkerSettings;

public sealed class WorkerPeriodSettings
{
    public const string Section = "WorkerSettings";

    public int PeriodInSeconds { get; init; }
}
