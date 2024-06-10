namespace Enterprise.Workers;

public interface IWorker
{
    WorkerConfig Config { get; }
    string WorkerId => Config.WorkerId;
    WorkerStatus Status { get; }
    Task<WorkerStatus> DoWorkAsync(IMessage message);
}