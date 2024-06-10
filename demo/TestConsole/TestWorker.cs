using Enterprise;
using Enterprise.Workers;

namespace TestConsole;

public sealed class TestWorker : WorkerBase
{
    public override Task DoWorkAsync(IMessage message)
    {
        throw new NotImplementedException();
    }
}