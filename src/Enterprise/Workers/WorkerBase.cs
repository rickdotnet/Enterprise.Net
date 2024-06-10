namespace Enterprise.Workers;

public abstract class WorkerBase : IWorker
{
    public WorkerStatus Status { get; protected set; }
    public abstract Task DoWorkAsync(IMessage message);
}