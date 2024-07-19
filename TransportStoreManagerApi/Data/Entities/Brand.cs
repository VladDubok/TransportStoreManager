namespace TransportStoreManagerApi.Data.Entities;

public class Brand : BaseEntity
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Model { get; set; }
    
    public List<Product> Products { get; set; }
}