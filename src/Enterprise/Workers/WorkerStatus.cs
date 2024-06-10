namespace Enterprise.Workers;

public record WorkerStatus
{
    public string WorkerId { get; init; } = string.Empty;
    public StatusCode StatusCode { get; init; }
    public string Status { get; init; } = string.Empty;
    public string Message { get; init; } = string.Empty;
    public DateTime? Timestamp { get; init; }
}

public enum StatusCode
{
    Unknown = 0,
    Available,
    Completed,
    Failed
}