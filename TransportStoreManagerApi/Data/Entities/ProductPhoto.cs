namespace TransportStoreManagerApi.Data.Entities;

public class ProductPhoto
{
    public long Id { get; set; }
    
    public long ProductId { get; set; }
    public Product Product { get; set; }
    public long FileId { get; set; }
    public File File { get; set; }
}