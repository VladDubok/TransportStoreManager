namespace TransportStoreManagerApi.Data.Entities;

public class File
{
    public long Id { get; set; }
    public byte[] Data { get; set; }

    public IEnumerable<ProductPhoto> ProductPhotos { get; set; }
}