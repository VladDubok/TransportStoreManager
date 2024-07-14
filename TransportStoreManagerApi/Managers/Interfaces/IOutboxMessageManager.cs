using TransportStoreManagerApi.Data.Entities;

namespace TransportStoreManagerApi.Managers.Interfaces;

public interface IOutboxMessageManager
{
    Func<OutboxMessage, Task> GetMessageHandler(string messageName);
}