namespace TransportStoreManagerApi.Data.Entities;

public class ProductPromotion
{
    public long Id { get; set; }
    
    public long ProductId { get; set; }
    public Product Product { get; set; }
    public long PromotionId { get; set; }
    public Promotion Promotion { get; set; }
    public IEnumerable<ProductPromotionHistory> ProductPromotionHistories { get; set; }
}