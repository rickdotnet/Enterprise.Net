using Enterprise;

namespace TestConsole;

public sealed class TestSystem : SystemBase
{
    private readonly SystemConfig config;

    public TestSystem(SystemConfig config)
    {
        this.config = config;
    }
}