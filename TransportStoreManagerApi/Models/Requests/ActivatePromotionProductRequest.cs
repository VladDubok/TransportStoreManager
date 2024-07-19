namespace TransportStoreManagerApi.Models.Requests;

public class ActivatePromotionProductRequest
{
    public long PromotionId { get; set; }
    public IEnumerable<long> ProductIds { get; set; }
    public bool Activate { get; set; }
}