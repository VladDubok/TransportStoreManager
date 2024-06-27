namespace TransportStoreManagerApi.Models.Requests;

public class AddProductRequestModel
{
    public string Color { get; set; }
    public int Year { get; set; }
    public decimal Price { get; set; }
    public long Count { get; set; }
    public long BrandId { get; set; }
    public long ProductTypeId { get; set; }
    public long CustomerId { get; set; }
    public long CurrencyId { get; set; }
}