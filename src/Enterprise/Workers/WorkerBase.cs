namespace Enterprise.Workers;

public abstract class WorkerBase : IWorker
{
    public WorkerConfig Config { get; }
    public WorkerStatus Status { get; protected set; }

    public async Task<WorkerStatus> DoWorkAsync(IMessage message)
    {
        // derive handle method based on message type
        var method = GetType().GetMethod("HandleAsync", new[] { message.GetType() });
        if (method is null)
            throw new InvalidOperationException($"No handler found for message type {message.GetType().Name}");
        
        // invoke the handler
        try
        {
            var task = (Task)method.Invoke(this, new object[] { message })!;
            await task;
            
            return new WorkerStatus
            {
                WorkerId = Config.WorkerId,
                StatusCode = StatusCode.Completed,
                Status = StatusCode.Completed.ToString(),
                Message = "Work completed successfully",
                Timestamp = DateTime.UtcNow
            };

        }
        catch (Exception ex)
        {
            return new WorkerStatus
            {
                WorkerId = Config.WorkerId,
                StatusCode = StatusCode.Failed,
                Status = StatusCode.Failed.ToString(),
                Message = ex.Message,
                Timestamp = DateTime.UtcNow
            };
        }

    }
}