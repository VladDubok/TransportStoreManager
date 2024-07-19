namespace TransportStoreManagerApi.Data.Entities;

public class ProductPromotion : BaseEntity
{
    public long Id { get; set; }
    
    public long ProductId { get; set; }
    public Product Product { get; set; }
    public long PromotionId { get; set; }
    public Promotion Promotion { get; set; }
    public bool IsActive { get; set; }
    
    public List<ProductPromotionHistory> ProductPromotionHistories { get; set; }
}