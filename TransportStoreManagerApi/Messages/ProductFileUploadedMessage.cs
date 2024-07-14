namespace TransportStoreManagerApi.Messages;

public class ProductFileUploadedMessage : IMessage
{
    public long FileId { get; set; }
    public long CustomerId { get; set; }

    public ProductFileUploadedMessage(long fileId, long customerId)
    {
        FileId = fileId;
        CustomerId = customerId;
    }
}