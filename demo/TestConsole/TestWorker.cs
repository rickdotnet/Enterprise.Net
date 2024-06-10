using Enterprise;
using Enterprise.Workers;

namespace TestConsole;

public sealed class TestWorker : WorkerBase
{
    public Task HandleAsync(SomeMessage message) 
        => Task.CompletedTask;

    public Task HandleAsync(SomeOtherMessage message) 
        => Task.CompletedTask;
}

public record SomeMessage : IMessage { }
public record SomeOtherMessage : IMessage { }