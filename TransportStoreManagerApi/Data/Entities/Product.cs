namespace TransportStoreManagerApi.Data.Entities;

public class Product : BaseEntity
{
    public long Id { get; set; }
    public string Color { get; set; }
    public int Year { get; set; }
    public long Count { get; set; }
    
    public long BrandId { get; set; }
    public Brand Brand { get; set; }
    public long ProductTypeId { get; set; }
    public ProductType ProductType { get; set; }
    public long CustomerId { get; set; }
    public Customer Customer { get; set; }
    
    public List<ProductPrice> ProductPrices { get; set; }
    public List<ProductPhoto> ProductPhotos { get; set; }
    public List<ProductPromotion> ProductPromotions { get; set; }
}