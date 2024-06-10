using Enterprise.Workers;

namespace Enterprise.Managers;

public interface IManager
{
    ManagerConfig Config { get; }
    
    void AddManager(IManager manager);
    void AddWorker(IWorker worker);

    //Task SendAsync(IMessage message);
    Task SendAsync<T>(T message) where T : IMessage;
}