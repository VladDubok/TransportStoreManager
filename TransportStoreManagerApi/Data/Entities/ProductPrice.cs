namespace TransportStoreManagerApi.Data.Entities;

public class ProductPrice : BaseEntity
{
    public long Id { get; set; }
    public string CurrencyCode { get; init; }
    public decimal Price { get; set; }

    public long ProductId { get; set; }
    public Product Product { get; set; }
}