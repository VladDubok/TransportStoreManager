namespace TransportStoreManagerApi.Handlers;

public interface IOutboxMessageProcessor
{
    Task Process();
}