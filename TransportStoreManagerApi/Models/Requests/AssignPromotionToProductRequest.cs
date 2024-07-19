namespace TransportStoreManagerApi.Models.Requests;

public class AssignPromotionToProductRequest
{
    public long PromotionId { get; set; }
    public IEnumerable<long> ProductIds { get; set; }
}