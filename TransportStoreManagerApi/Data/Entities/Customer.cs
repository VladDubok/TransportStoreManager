namespace TransportStoreManagerApi.Data.Entities;

public class Customer : BaseEntity
{
    public long Id { get; set; }
    public string Fullname { get; set; }

    public IEnumerable<Product> Products { get; set; }
}