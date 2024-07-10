namespace TransportStoreManagerApi.Data.Entities;

public class OutboxMessage : BaseEntity
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Data { get; set; }
    public bool IsProcessed { get; set; }
}