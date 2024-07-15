
namespace TransportStoreManagerApi.Models.Responses;

public class GetCustomerProductResponseModel
{
    public long Id { get; set; }
    public string BrandName { get; set; }
    public string BrandModel { get; set; }
    public string ProductTypeName { get; set; }
    public string Color { get; set; }
    public int Year { get; set; }
    public long Count { get; set; }
    public IEnumerable<ProductPriceModel> Prices { get; set; }
}

public class ProductPriceModel
{
    public decimal Price { get; set; }
    public string CurrencyCode { get; set; }
}