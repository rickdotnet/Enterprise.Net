using Enterprise.Managers;
using Enterprise.Workers;

namespace Enterprise;

public class SystemBase : ISystem
{
   
    public void AddManager<T>(T manager) where T : class, IManager
    {
        // the system builder is going to pass instances of managers.
        // managers will hold instances of their managers/workers.
        // this system is responsible for building a channel/queue foreach top-level manager
       
    }
    
    public void AddWorker<T>(T worker) where T : class, IWorker
    {
        // the system builder is going to pass instances of workers
        // this system is responsible for building a channel/queue for community
        // workers. this method will add available workers to the system that will
        // process messages as they are available
    }

    public Task SendAsync<T>(T message) where T : IMessage
    {
        throw new NotImplementedException();
    }

    public Task AssignWorkAsync<T>(T message) where T : IMessage
    {
        throw new NotImplementedException();
    }
}