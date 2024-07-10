namespace TransportStoreManagerApi.Data.Entities;

public class ProductType : BaseEntity
{
    public long Id { get; set; }
    public string Name { get; set; }
    
    public IEnumerable<Product> Products { get; set; }
}