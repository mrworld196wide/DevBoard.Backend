

namespace DevBoard.Application.Interfaces
{
    public interface IEventPublisher
    {
        Task PublishAsync<T>(string routingKey, T message);
    }
}
