namespace TransportStoreManagerApi.Models.Dtos;

public class FileProductDto
{
    public long? Id { get; set; }
    public string Color { get; set; }
    public int Year { get; set; }
    public decimal Price { get; set; }
    public long Count { get; set; }
    public long CustomerId { get; set; }
    public long ProductTypeId { get; set; }
    public string CurrencyName { get; set; }
    public string BrandName { get; set; }
}