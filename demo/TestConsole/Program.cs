using Enterprise;
using Microsoft.Extensions.Hosting;
using TestConsole;

var systemConfig = new SystemConfig();
var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddEnterprise(enterprise =>
{
    enterprise.AddSystem<TestSystem>(
        systemConfig,
        system =>
        {
            // managers can add workers during registration
            system.AddManager<TestManager>(cfg => cfg with { CommunityManager = true })
                  .WithWorker<TestWorker>();
            
            // system-wide workers can be added and used by all managers
            system.AddWorker<TestWorker>(cfg => cfg with { WorkerId = "test-worker" });
        });
});