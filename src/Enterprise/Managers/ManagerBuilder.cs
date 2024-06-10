using Enterprise.Workers;

namespace Enterprise.Managers;

public class ManagerBuilder
{
    private readonly SystemBuilder systemBuilder;
    private readonly ManagerConfig config;
    private Dictionary<string, ManagerBuilder> managers = new();
    private Dictionary<string, WorkerConfig> workers = new();
    public string ManagerId => config.ManagerId;
    
    public ManagerBuilder(SystemBuilder systemBuilder, ManagerConfig config)
    {
        this.systemBuilder = systemBuilder;
        this.config = config;
    }
    
    public ManagerBuilder WithManager<T>(Func<ManagerConfig,ManagerConfig>? configFunc = null) where T : class, IManager
    {
        var uniqueId = Guid.NewGuid().ToString();
        var managerConfig = new ManagerConfig(uniqueId, CommunityManager: false);
        if(configFunc != null)
            managerConfig = configFunc.Invoke(managerConfig);
        
        var manager = systemBuilder.AddManager<T>(managerConfig);
        managers.Add(manager.ManagerId, manager);
        return this;
    }
    public ManagerBuilder WithWorker<T>(Func<WorkerConfig,WorkerConfig>? configFunc = null) where T : class, IWorker
    {
        var workerConfig = new WorkerConfig(typeof(T).Name, CommunityWorker: false);
        if(configFunc != null)
            workerConfig = configFunc.Invoke(workerConfig);
        
        systemBuilder.AddWorker<T>(workerConfig);
        workers.Add(workerConfig.WorkerId, workerConfig);
        return this;
    }

    public IManager Build()
    {
        // return a new manager instance
        throw new NotImplementedException();
    }
}