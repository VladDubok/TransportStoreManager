namespace TransportStoreManagerApi.Messages;

public class ProductFileUploadedMessage
{
    public long FileId { get; set; }

    public ProductFileUploadedMessage(long fileId)
    {
        FileId = fileId;
    }
}