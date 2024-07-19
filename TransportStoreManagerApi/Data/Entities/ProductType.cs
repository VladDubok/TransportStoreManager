namespace TransportStoreManagerApi.Data.Entities;

public class ProductType : BaseEntity
{
    public long Id { get; set; }
    public string Name { get; set; }
    
    public List<Product> Products { get; set; }
}