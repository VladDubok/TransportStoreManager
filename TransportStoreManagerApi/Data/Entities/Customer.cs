namespace TransportStoreManagerApi.Data.Entities;

public class Customer
{
    public long Id { get; set; }
    public string Fullname { get; set; }

    public IEnumerable<Product> Products { get; set; }
}