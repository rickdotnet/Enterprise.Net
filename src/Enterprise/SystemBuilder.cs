using System.Collections.Concurrent;
using Enterprise.Managers;
using Enterprise.Workers;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Enterprise;

public class SystemBuilder
{
    private readonly EnterpriseBuilder enterpriseBuilder;
    private readonly SystemConfig config;

    private ConcurrentDictionary<Type, List<ManagerBuilder>> managerBuilders = new();

    public SystemBuilder(EnterpriseBuilder enterpriseBuilder, SystemConfig config)
    {
        this.enterpriseBuilder = enterpriseBuilder;
        this.config = config;
    }

    internal void AddSystem<T>() where T : class, ISystem
    {
        enterpriseBuilder.Services.TryAddSingleton<T>();
    }

    public ManagerBuilder AddManager<T>(Func<ManagerConfig, ManagerConfig> configFunc) where T : class, IManager
    {
        var managerConfig = new ManagerConfig(typeof(T).Name, false);
        managerConfig = configFunc(managerConfig);
        return AddManager<T>(managerConfig);
    }

    public ManagerBuilder AddManager<T>(ManagerConfig? managerConfig = null) where T : class, IManager
    {
        managerConfig ??= new ManagerConfig(typeof(T).Name, false);

        // add to DI as transient to create multiple instances
        // the system will create instances of managers/workers
        // managers will hold instances of their managers/workers
        enterpriseBuilder.Services.TryAddTransient<T>();

        // add queue/channel
        // track the builders by type
        var builder = new ManagerBuilder(this, managerConfig);
        managerBuilders.AddOrUpdate(typeof(T),
            _ => [builder],
            (_, list) =>
            {
                // multiple subscribers could register and subscribe to the same message type
                // subscribers will be derived based on the types they handle
                var skip = list.Any(x => x.ManagerId == managerConfig.ManagerId);
                if (skip)
                    throw new Exception("Did we hit this? Hope not. ;)"); //return list;

                list.Add(builder);
                return list;
            });

        return builder;
    }

    public void AddWorker<T>(Func<WorkerConfig, WorkerConfig> configFunc) where T : class, IWorker
    {
        var workerConfig = new WorkerConfig(typeof(T).Name);
        workerConfig = configFunc(workerConfig);
        AddWorker<T>(workerConfig);
    }

    public void AddWorker<T>(WorkerConfig? workerConfig = null) where T : class, IWorker
    {
        // add to DI as transient to create multiple instances
        // // the system will create instances of managers/workers
        // // managers will hold instances of their managers/workers
        // enterpriseBuilder.Services.TryAddTransient<T>();
        enterpriseBuilder.Services.TryAddTransient<T>();

        // add queue/channel
    }
}