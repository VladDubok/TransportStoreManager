namespace TransportStoreManagerApi.Models.Requests;

public class UpdatePromotionRequest
{
    public long Id { get; set; }
    public int Percent { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }
}