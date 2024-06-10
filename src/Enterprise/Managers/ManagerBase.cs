using Enterprise.Workers;

namespace Enterprise.Managers;

public class ManagerBase : IManager
{
    public ManagerConfig Config { get; }
    
    protected ISystem System { get; }
    protected IReadOnlyList<IManager> Managers { get; }
    protected IReadOnlyList<IWorker> Workers { get; }
}