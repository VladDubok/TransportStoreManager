using CsvHelper.Configuration.Attributes;

namespace TransportStoreManagerApi.Models.Dtos;

public class UploadFileProductDto
{
    [Name("Id")]
    public long? Id { get; set; }
    
    [Name("Color")]
    public string Color { get; set; }
    
    [Name("Year")]
    public int Year { get; set; }
    
    [Name("Price")]
    public decimal Price { get; set; }
    
    [Name("CurrencyCode")]
    public string CurrencyCode { get; set; }
    
    [Name("Count")]
    public long Count { get; set; }
    
    [Ignore]
    public long CustomerId { get; set; }
    
    [Name("ProductTypeId")]
    public long ProductTypeId { get; set; }
    
    [Name("BrandName")]
    public string BrandName { get; set; }
    
    [Name("BrandModel")]
    public string BrandModel { get; set; }
}