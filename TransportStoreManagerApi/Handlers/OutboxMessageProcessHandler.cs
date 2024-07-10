using TransportStoreManagerApi.Data.Entities;
using TransportStoreManagerApi.Managers.Interfaces;
using TransportStoreManagerApi.Repositories.Interfaces;

namespace TransportStoreManagerApi.Handlers;

public class OutboxMessageProcessHandler : IOutboxMessageProcessHandler
{
    private readonly IBaseRepository<OutboxMessage> _outboxRepository;
    private readonly IOutboxMessageManager _outboxMessageManager;

    public OutboxMessageProcessHandler(IBaseRepository<OutboxMessage> outboxRepository, IOutboxMessageManager outboxMessageManager)
    {
        _outboxRepository = outboxRepository;
        _outboxMessageManager = outboxMessageManager;
    }

    public async Task Handle()
    {
        var messages = await _outboxRepository.GetAllAsync(x => !x.IsProcessed);

        foreach (var outboxMessage in messages)
        {
            var strategy = _outboxMessageManager.GetStrategy(outboxMessage.Name);
            await strategy.Invoke(outboxMessage);
            outboxMessage.IsProcessed = true;
            await _outboxRepository.UpdateAsync(outboxMessage);
        }
    }
}