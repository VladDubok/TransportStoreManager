namespace TransportStoreManagerApi.Models.Requests;

public class UpdateProductRequestModel
{
    public string Color { get; set; }
    public int Year { get; set; }
    public long Count { get; set; }
    public decimal Price { get; set; }
    public string CurrencyCode { get; set; }
    public string BrandName { get; set; }
    public string BrandModel { get; set; }
    public long ProductTypeId { get; set; }
    public long CustomerId { get; set; }
}