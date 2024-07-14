namespace TransportStoreManagerApi.Models.Responses;

public class GetCustomerProductResponseModel
{
    public string BrandName { get; set; }
    public string BrandModel { get; set; }
    public string ProductTypeName { get; set; }
    public string Color { get; set; }
    public int Year { get; set; }
    public long Count { get; set; }
}