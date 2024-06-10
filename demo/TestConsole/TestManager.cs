using Enterprise;
using Enterprise.Managers;

namespace TestConsole;

public record TestMessage(string Message) : IMessage;
public class TestManager : ManagerBase
{
    public async Task Test()
    {
        await System.SendAsync(new TestMessage("Hello"));
        
    }
}

