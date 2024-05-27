namespace TransportStoreManagerApi.Data.Entities;

public class OutboxMessages
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Data { get; set; }
}