namespace Enterprise.Workers;

public abstract class WorkerBase : IWorker
{
    public WorkerConfig Config { get; }
    public WorkerStatus Status { get; protected set; }

    public Task DoWorkAsync(IMessage message)
    {
        // derive handle method based on message type
        var method = GetType().GetMethod("HandleAsync", new[] { message.GetType() });
        if (method is null)
            throw new InvalidOperationException($"No handler found for message type {message.GetType().Name}");
        
        // invoke the handler
        return (Task)method.Invoke(this, new object[] { message })!;
    }
}