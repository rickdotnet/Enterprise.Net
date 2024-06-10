namespace Enterprise.Workers;

public interface IWorker
{
    WorkerStatus Status { get; }
    Task DoWorkAsync(IMessage message);
}