namespace TransportStoreManagerApi.Data.Entities;

public class BlobFile : BaseEntity
{
    public long Id { get; set; }
    public byte[] Data { get; set; }

    public List<ProductPhoto> ProductPhotos { get; set; }
}