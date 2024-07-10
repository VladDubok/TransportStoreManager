namespace TransportStoreManagerApi.Data.Entities;

public class Currency : BaseEntity
{
    public long Id { get; set; }
    public string Name { get; set; }

    public IEnumerable<Product> Products { get; set; }
}