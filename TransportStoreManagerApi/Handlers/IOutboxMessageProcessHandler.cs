namespace TransportStoreManagerApi.Handlers;

public interface IOutboxMessageProcessHandler
{
    Task Handle();
}