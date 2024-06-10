using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Enterprise;

public static class EnterpriseBuilderExtensions
{
    
    public static IServiceCollection AddEnterprise(this IServiceCollection services, Action<EnterpriseBuilder> builderFactory)
    {
        var builder = new EnterpriseBuilder(services);
        builderFactory(builder);
        
        return services;
    }
}
public class EnterpriseBuilder
{
    internal IServiceCollection Services { get; }

    public EnterpriseBuilder(IServiceCollection serviceCollection)
    {
        this.Services = serviceCollection;
    }
    public EnterpriseBuilder AddSystem<T>(SystemConfig? config = null, Action<SystemBuilder>? builderAction = null) where T : class, ISystem
    {
        config ??= SystemConfig.Default;

        var systemBuilder = new SystemBuilder(this, config);
        systemBuilder.AddSystem<T>();
        
        builderAction?.Invoke(systemBuilder);
        
        
        
        return this;
    }
}