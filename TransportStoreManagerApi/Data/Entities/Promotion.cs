namespace TransportStoreManagerApi.Data.Entities;

public class Promotion
{
    public long Id { get; set; }
    public int Percent { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public IEnumerable<ProductPromotion> ProductPromotions { get; set; }
}