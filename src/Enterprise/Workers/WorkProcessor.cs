using System.Threading.Channels;

namespace Enterprise.Workers;

public class WorkProcessor
{
    private readonly IWorker worker;
    private readonly Channel<IMessage> workerChannel = Channel.CreateUnbounded<IMessage>();
    private List<Task> processTasks = new();
    private Task workerTask;
    public WorkProcessor(IWorker worker)
    {
        this.worker = worker;
        workerTask = Task.Run(async () =>
        {
            await foreach (var message in workerChannel.Reader.ReadAllAsync())
            {
                await worker.DoWorkAsync(message);
            }
        });
    }
    public void ProcessChannel(Channel<IMessage> typeChannel)
    {
        // add the channel to the worker's list of channels
        var processTask = Task.Run(async () =>
        {
            await foreach (var message in typeChannel.Reader.ReadAllAsync())
            {
                await WriteAsync(message);
            }
        });
        
        processTasks.Add(processTask);
    }
    private ValueTask WriteAsync(IMessage message)
    {
        return workerChannel.Writer.WriteAsync(message);
    }
    
}