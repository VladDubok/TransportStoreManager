namespace TransportStoreManagerApi.Data.Entities;

public class ProductPromotionHistory
{
    public long Id { get; set; }
    public int Percent { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime Created { get; set; }
    public bool IsActive { get; set; }
    
    public long ProductPromotionId { get; set; }
    public ProductPromotion ProductPromotion { get; set; }
}