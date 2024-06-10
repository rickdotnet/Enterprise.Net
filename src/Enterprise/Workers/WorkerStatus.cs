namespace Enterprise.Workers;

public record WorkerStatus
{
    public string WorkerId { get; init; }
    public string Status { get; init; }
    public string Message { get; init; }
    public DateTime Timestamp { get; init; }
}