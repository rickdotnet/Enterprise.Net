using Enterprise;
using Enterprise.Managers;
using Enterprise.Workers;

namespace TestConsole;

public record TestMessage(string Message) : IMessage;
public class TestManager(ManagerConfig config): ManagerBase(config)
{
    public async Task Test()
    {
      
    }
}

