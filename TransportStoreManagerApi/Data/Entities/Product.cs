namespace TransportStoreManagerApi.Data.Entities;

public class Product
{
    public long Id { get; set; }
    public string Color { get; set; }
    public int Year { get; set; }
    public decimal Price { get; set; }
    public long Count { get; set; }
    
    public long BrandId { get; set; }
    public Brand Brand { get; set; }
    public long ProductTypeId { get; set; }
    public ProductType ProductType { get; set; }
    public long CustomerId { get; set; }
    public Customer Customer { get; set; }
    
    public IEnumerable<ProductPhoto> ProductPhotos { get; set; }
    public IEnumerable<Currency> Currencies { get; set; }
}