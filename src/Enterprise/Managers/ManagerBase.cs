using System.Threading.Channels;
using Enterprise.Workers;

namespace Enterprise.Managers;

public class ManagerBase : IManager
{
    public ManagerConfig Config { get; }
    private ISystem? system;
    
    // worker channel
    private readonly Channel<IMessage> workerMessages = Channel.CreateUnbounded<IMessage>();
    private Dictionary<string, WorkProcessor> workers = new(); 
    private Dictionary<Type,Channel<IMessage>> workerMessageChannels = new();
    public ManagerBase(ManagerConfig config) => Config = config;

    public void AddManager(IManager manager)
    {
        throw new NotImplementedException();
    }

    public void AddWorker(IWorker worker)
    {
        // add worker to the dictionary
        var workProcessor = new WorkProcessor(worker);
        workers.Add(worker.WorkerId, workProcessor);
        
        // get the worker's message type(s)
        var messageTypes = Array.Empty<Type>(); //worker.GetMessageTypes();
        foreach (var messageType in messageTypes)
        {
            var channel = workerMessageChannels.GetValueOrDefault(messageType);
            channel ??= Channel.CreateUnbounded<IMessage>();
                    
            // make sure the worker processes the channel
            workProcessor.ProcessChannel(channel);
            workerMessageChannels.Add(messageType, channel);
        }
    }
    internal void SetSystem(ISystem system)
    {
        this.system = system;
    }

    public async Task SendAsync<T>(T message) where T : IMessage 
    {
        // if it's a worker message
        if(workerMessageChannels.ContainsKey(message.GetType()))
            await workerMessages.Writer.WriteAsync(message);
        
        // if it's a manager message
        
        
        throw new NotImplementedException();
    }

    public Task AssignWorkAsync(IMessage message)
    {
        var worker = GetAvailableWorker();
        throw new NotImplementedException();
    }
    
    private IWorker? GetAvailableWorker()
    {
        
        throw new NotImplementedException();
    }
}