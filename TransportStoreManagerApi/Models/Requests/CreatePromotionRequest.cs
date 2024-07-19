namespace TransportStoreManagerApi.Models.Requests;

public class CreatePromotionRequest
{
    public int Percent { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}