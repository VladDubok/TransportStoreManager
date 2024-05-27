namespace TransportStoreManagerApi.Data.Entities;

public class Brand
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Model { get; set; }
    
    public IEnumerable<Product> Products { get; set; }
}