namespace Enterprise;

public record SystemConfig
{
    public static readonly SystemConfig Default = new();

}

public interface IConfigFor<T>
{
    SystemConfig Config { get; }
}
public record ConfigFor<T>(SystemConfig Config) : IConfigFor<T>;
