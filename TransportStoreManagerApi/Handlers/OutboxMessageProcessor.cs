using TransportStoreManagerApi.Data.Entities;
using TransportStoreManagerApi.Data.Entities.Enums;
using TransportStoreManagerApi.Managers.Interfaces;
using TransportStoreManagerApi.Repositories.Interfaces;

namespace TransportStoreManagerApi.Handlers;

public class OutboxMessageProcessor : IOutboxMessageProcessor
{
    private readonly IBaseRepository<OutboxMessage> _outboxRepository;
    private readonly IOutboxMessageManager _outboxMessageManager;

    public OutboxMessageProcessor(IBaseRepository<OutboxMessage> outboxRepository, IOutboxMessageManager outboxMessageManager)
    {
        _outboxRepository = outboxRepository;
        _outboxMessageManager = outboxMessageManager;
    }

    public async Task Process()
    {
        var messages = await _outboxRepository.GetAllAsync(x => x.Status == MessageStatusEnum.Created);

        foreach (var outboxMessage in messages)
        {
            try
            {
                outboxMessage.Status = MessageStatusEnum.Pending;
                await _outboxRepository.UpdateAsync(outboxMessage);
                
                var strategy = _outboxMessageManager.GetMessageHandler(outboxMessage.Name);
                await strategy.Invoke(outboxMessage);
                
                outboxMessage.Status = MessageStatusEnum.Success;
                await _outboxRepository.UpdateAsync(outboxMessage);
            }
            catch (Exception e)
            {
                outboxMessage.Status = MessageStatusEnum.Failed;
                outboxMessage.Reason = e.Message;
                await _outboxRepository.UpdateAsync(outboxMessage);
            }
        }
    }
}