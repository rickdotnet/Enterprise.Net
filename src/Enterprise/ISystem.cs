using Enterprise.Managers;
using Microsoft.Extensions.DependencyInjection;

namespace Enterprise;

public interface ISystem
{
    Task SendAsync<T>(T message) where T : IMessage;
}